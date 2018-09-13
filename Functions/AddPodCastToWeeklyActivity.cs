using Microsoft.Azure.WebJobs;
using Red_Folder.ActivityTracker.Models;
using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class AddPodCastToWeeklyActivityx
    {
        [FunctionName("AddPodCastToWeeklyActivity")]
        public static async System.Threading.Tasks.Task RunAsync(
            [QueueTrigger("podcast-to-be-added-to-weekly-activity", Connection = "AzureWebJobsStorage")]PodCast podCast,
            Binder binder,
            ILogger log)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;

            var dated = podCast.Created;
            int weekNumber = culture.Calendar.GetWeekOfYear(
                                dated,
                                CalendarWeekRule.FirstDay,
                                DayOfWeek.Monday);

            int year = weekNumber == 52 && dated.Month == 1 ? dated.Year - 1 : dated.Year;

            var blobName = $"activity-weekly/{year.ToString("0000")}/{weekNumber.ToString("00")}.json";

            log.LogInformation($"To save to {blobName}");

            var attributes = new Attribute[]
            {
                new BlobAttribute(blobName),
                new StorageAccountAttribute("AzureWebJobsStorage")
            };

            var blob = await binder.BindAsync<CloudBlockBlob>(attributes);
            var exists = await blob.ExistsAsync();
            if (!exists)
            {
                log.LogInformation("Blob not exist - create temporary");
                await blob.UploadTextAsync("");
            }
            string lease = await blob.AcquireLeaseAsync(TimeSpan.FromSeconds(15), null);
            var accessCondition = AccessCondition.GenerateLeaseCondition(lease);

            var json = await blob.DownloadTextAsync();
            var week = json.Length == 0 ?
                            new WeekActivity(year, weekNumber) :
                            JsonConvert.DeserializeObject<WeekActivity>(json);

            week.AddPodCast(podCast);

            await blob.UploadTextAsync(JsonConvert.SerializeObject(week), null, accessCondition, null, null);
            await blob.ReleaseLeaseAsync(accessCondition);
        }
    }
}
