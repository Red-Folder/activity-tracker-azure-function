using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker
{
    public static class CaptureActivityImage
    {
        [FunctionName("CaptureActivityImage")]
        public async static Task<byte[]> Run([ActivityTrigger] DurableActivityContext context, ILogger log)
        {
            var week = context.GetInput<Week>();
            log.LogInformation($"Running Capture Activity Image for week #{week.WeekNumber}");

            var accessKey = Environment.GetEnvironmentVariable("ScreenshotLayerAccessKey", EnvironmentVariableTarget.Process);
            var url = $"http://api.screenshotlayer.com/api/capture?access_key={accessKey}&url=https://red-folder.com/activity/weekly/{week.Year.ToString("0000")}/{week.WeekNumber.ToString("00")}&fullpage=1&force=1&delay=2";

            var client = new HttpClient();

            var response = await client.GetByteArrayAsync(url);

            return response;
        }
    }
}
