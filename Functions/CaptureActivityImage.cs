using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class CaptureActivityImage
    {
        [FunctionName("CaptureActivityImage")]
        public async static Task<byte[]> Run([ActivityTrigger] DurableActivityContext context, ILogger log)
        {
            var week = context.GetInput<Week>();
            log.LogInformation($"Running Capture Activity Image for week #{week.WeekNumber}");

            var url = "https://mbtskoudsalg.com/images/example-stamp-png-2.png";

            var client = new HttpClient();

            var response = await client.GetByteArrayAsync(url);

            return response;
        }
    }
}
