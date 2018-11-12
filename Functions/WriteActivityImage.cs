using Microsoft.Azure.WebJobs;
using Red_Folder.ActivityTracker.Models;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class WriteActivityImage
    {
        [FunctionName("WriteActivityImage")]
        public async static void Run([ActivityTrigger]  DurableActivityContext context, Binder binder, ILogger log)
        {
            var activityImage = context.GetInput<ActivityImage>();
            var week = activityImage.Week;
            var imageData = activityImage.ImageData;

            var blobName = $"activity-weekly/{week.Year.ToString("0000")}/{week.WeekNumber.ToString("00")}.png";

            log.LogInformation($"Preparing to save {blobName}");
            var attributes = new Attribute[]
            {
                new BlobAttribute(blobName),
                new StorageAccountAttribute("ContentStorage")
            };

            var blob = await binder.BindAsync<CloudBlockBlob>(attributes);

            await blob.UploadFromByteArrayAsync(imageData, 0, imageData.Length);

            log.LogInformation("Image saved");
        }
    }
}
