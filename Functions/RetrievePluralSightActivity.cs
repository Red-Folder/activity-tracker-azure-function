using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Services;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class RetrievePluralsightActivity
    {
        [FunctionName("RetrievePluralsightActivity")]
        public static async System.Threading.Tasks.Task RunAsync(
            [TimerTrigger("0 15 10 * * 1")]TimerInfo myTimer,
            Binder binder,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var pluralsightId = Environment.GetEnvironmentVariable("PluralsightId", EnvironmentVariableTarget.Process);
            var overrideWeek = Environment.GetEnvironmentVariable("OverrideWeek", EnvironmentVariableTarget.Process);

            var week = Week.LastWeek();
            if (!String.IsNullOrEmpty(overrideWeek))
            {
                log.LogInformation($"Override set for week: {overrideWeek}");
                var weekNumber = int.Parse(overrideWeek.Split(':')[0]);
                var year = int.Parse(overrideWeek.Split(':')[1]);

                week = Week.FromYearAndWeekNumber(year, weekNumber);
            }

            var proxy = new PluralsightProxy(log);

            log.LogInformation($"Retrieving data for ${week.Start.ToShortDateString()} to ${week.End.ToShortDateString()}");
            await proxy.PopulateAsync(pluralsightId, week.Start, week.End);

            log.LogInformation($"Get the Pluralsight activitys");
            var pluralsightActivity = proxy.GetPluralsightActivity();

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

                activity.Pluralsight = pluralsightActivity;

                log.LogInformation("Uploading blob");
                await locker.Upload(activity);
            }

            log.LogInformation("Function complete");
        }

    }
}
