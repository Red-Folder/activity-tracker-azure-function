using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class PrepareWeeklyActivity
    {
        [FunctionName("PrepareWeeklyActivity")]
        public async static Task Run( [TimerTrigger("0 30 10 * * 1")]TimerInfo myTimer,
                                DurableOrchestrationContext context,
                                ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var week = await context.CallActivityAsync<Week>("GetWeek", null);
            log.LogInformation($"Running for week ${week.WeekNumber}");

            log.LogInformation($"Run complete");
        }
    }
}
