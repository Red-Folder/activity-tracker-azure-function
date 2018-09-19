using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Models.Toggl;
using Red_Folder.ActivityTracker.Services;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class RetrieveTogglActivity
    {
        [FunctionName("RetrieveTogglActivity")]
        public static async System.Threading.Tasks.Task RunAsync(
            [TimerTrigger("0 0 10 * * 1")]TimerInfo myTimer,
            Binder binder,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var togglApiKey = Environment.GetEnvironmentVariable("TogglApiKey", EnvironmentVariableTarget.Process);
            var togglWorkspaceId = Environment.GetEnvironmentVariable("TogglWorkspaceId", EnvironmentVariableTarget.Process);

            var week = Week.LastWeek();

            var proxy = new TogglProxy(log);

            log.LogInformation($"Retrieving data for ${week.Start.ToShortDateString()} to ${week.End.ToShortDateString()}");
            await proxy.PopulateAsync(togglApiKey, togglWorkspaceId, week.Start, week.End);

            log.LogInformation($"Get the Skills activitys");
            var skillActivity = proxy.GetSkillsActivity();

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

                activity.Skills = skillActivity;

                log.LogInformation("Uploading blob");
                await locker.Upload(activity);
            }

            log.LogInformation("Function complete");
        }

    }
}
