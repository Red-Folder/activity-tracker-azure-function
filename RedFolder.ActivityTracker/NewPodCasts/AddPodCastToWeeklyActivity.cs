using Microsoft.Azure.WebJobs;
using RedFolder.ActivityTracker.Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using RedFolder.ActivityTracker.Services;

namespace RedFolder.ActivityTracker.NewPodCasts
{
    public static class AddPodCastToWeeklyActivityx
    {
        [FunctionName("AddPodCastToWeeklyActivity")]
        public static async System.Threading.Tasks.Task RunAsync(
            [QueueTrigger("podcast-to-be-added-to-weekly-activity", Connection = "AzureWebJobsStorage")]PodCast podCast,
            Binder binder,
            ILogger log)
        {
            var week = Week.FromDate(podCast.Created);

            var blobName = $"activity-weekly/{week.Year.ToString("0000")}/{week.WeekNumber.ToString("00")}.json";

            log.LogInformation($"To save to {blobName}");

            var attributes = new Attribute[]
            {
                new BlobAttribute(blobName),
                new StorageAccountAttribute("AzureWebJobsStorage")
            };

            var blob = await binder.BindAsync<CloudBlockBlob>(attributes);

            using (var locker = new CloudBlockBlobLocker<Models.WeekActivity>(blob))
            {
                var activity = locker.IsNew ?
                                new Models.WeekActivity(week.Year, week.WeekNumber) :
                                await locker.Download();

                activity.AddPodCast(podCast);

                await locker.Upload(activity);
            }
        }
    }
}
