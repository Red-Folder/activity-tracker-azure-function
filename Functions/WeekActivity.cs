using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Functions
{
    public class WeekActivity
    {
        [FunctionName("WeeklyActivity")]
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "weeklyactivity/{year}/{weekNumber}")]HttpRequest req,
            int? year, 
            int? weekNumber,
            [Blob("activity-weekly/{year}/{weekNumber}.json", Connection = "AzureWebJobsStorage")]string weeklyActivity,
            ILogger log)
        {
            log.LogInformation($"Weekyl Activity requested for Year: {year}, Week Number: {weekNumber}");

            if (String.IsNullOrEmpty(weeklyActivity))
            {
                return new NotFoundObjectResult($"Weekly activity for {year} & {weekNumber} not found");
            }

            return new OkObjectResult(weeklyActivity);
        }
    }
}
