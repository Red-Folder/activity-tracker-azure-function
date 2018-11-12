using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Red_Folder.ActivityTracker.Services;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class SaveActivity
    {
        [FunctionName("SaveActivity")]
        public async static Task Run([ActivityTrigger] DurableActivityContext context, Binder binder, ILogger log)
        {
            var activityToSave = context.GetInput<Models.WeekActivity>();

            var blobName = $"activity-weekly/{activityToSave.Year.ToString("0000")}/{activityToSave.WeekNumber.ToString("00")}.json";

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
                if (locker.IsNew)
                {
                    log.LogInformation("Uploading new blob");
                    await locker.Upload(activityToSave);
                }
                else
                {
                    var mergedActivity = await locker.Download();

                    if (activityToSave.Blogs != null) mergedActivity.Blogs = activityToSave.Blogs;
                    if (activityToSave.Clients != null) mergedActivity.Clients = activityToSave.Clients;
                    if (activityToSave.Focus != null) mergedActivity.Focus = activityToSave.Focus;
                    if (activityToSave.Pluralsight != null) mergedActivity.Pluralsight = activityToSave.Pluralsight;
                    if (activityToSave.PodCasts != null) mergedActivity.PodCasts = activityToSave.PodCasts;
                    if (activityToSave.Skills != null) mergedActivity.Skills = activityToSave.Skills;

                    log.LogInformation("Uploading merged blob");
                    await locker.Upload(mergedActivity);
                }
            }
        }
    }
}
