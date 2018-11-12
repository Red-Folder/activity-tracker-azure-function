using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class OrchestrateWeeklyActivity
    {
        [FunctionName("OrchestrateWeeklyActivity")]
        public async static Task Run([OrchestrationTrigger] DurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var week = await context.CallActivityAsync<Week>("GetWeek", null);
            log.LogInformation($"Running for week ${week.WeekNumber}");

            var imageData = await context.CallActivityAsync<byte[]>("CaptureActivityImage", week);

            var activityImage = new ActivityImage
            {
                Week = week,
                ImageData = imageData
            };
            await context.CallActivityAsync("WriteActivityImage", activityImage);

            log.LogInformation($"Run complete");
        }
    }
}
