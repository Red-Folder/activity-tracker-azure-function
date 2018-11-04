using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Services;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class RetrieveBlogActivity
    {
        [FunctionName("RetrieveBlogActivity")]
        public static async System.Threading.Tasks.Task RunAsync(
            [TimerTrigger("0 30 10 * * 1")]TimerInfo myTimer,
            //[TimerTrigger("0 * * * * *")]TimerInfo myTimer,
            Binder binder,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var blogUrl = Environment.GetEnvironmentVariable("BlogUrl", EnvironmentVariableTarget.Process);

            //var week = Week.LastWeek();
            var week = Week.Current();

            var proxy = new BlogProxy(log);

            log.LogInformation($"Retrieving Blog Activity for ${week.Start.ToShortDateString()} to ${week.End.ToShortDateString()}");
            var blogActivity = await proxy.GetBlogActivityAsync(blogUrl, week.Start, week.End);

            var blobName = $"activity-weekly/{week.Year.ToString("0000")}/{week.WeekNumber.ToString("00")}.json";

            log.LogInformation($"Preparing to update ${blobName}");
            var attributes = new Attribute[]
            {
                new BlobAttribute(blobName),
                new StorageAccountAttribute("AzureWebJobsStorage")
            };

            var blob = await binder.BindAsync<CloudBlockBlob>(attributes);

            log.LogInformation("Locking blob");
            using (var locker = new CloudBlockBlobLocker<Models.WeekActivity>(blob))
            {
                var activity = locker.IsNew ?
                                new Models.WeekActivity(week.Year, week.WeekNumber) :
                                await locker.Download();

                activity.Blogs = blogActivity;

                log.LogInformation("Uploading blob");
                await locker.Upload(activity);
            }

            log.LogInformation("Function complete");
        }

    }
}
