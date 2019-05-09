using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;

namespace RedFolder.ActivityTracker
{
    public class WeekActivity
    {
        [FunctionName("WeeklyActivity")]
        public static IActionResult RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "weeklyactivity/{year}/{weekNumber}")]HttpRequest req,
            string year, 
            string weekNumber,
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
