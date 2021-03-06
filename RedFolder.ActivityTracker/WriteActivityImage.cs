using Microsoft.Azure.WebJobs;
using RedFolder.ActivityTracker.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.Storage.Blob;

namespace RedFolder.ActivityTracker
{
    public static class WriteActivityImage
    {
        [FunctionName("WriteActivityImage")]
        public async static Task<string> Run([ActivityTrigger]  IDurableActivityContext context, Binder binder, ILogger log)
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

            return blobName;
        }
    }
}
