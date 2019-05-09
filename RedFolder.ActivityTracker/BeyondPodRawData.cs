using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;

namespace RedFolder.ActivityTracker
{
    public class BeyondPodRawData
    {
        [FunctionName("BeyondPodRawData")]
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync(
                                [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req,
                                [Queue("beyond-pod-raw-data", Connection= "AzureWebJobsStorage")]ICollector<string> outputQueueItem,
                                ILogger log)
        {
            log.LogInformation("BeyondPodRawData function triggered");

            var jsonString = await req.ReadAsStringAsync();
            if (String.IsNullOrEmpty(jsonString))
            {
                log.LogWarning("Received empty string");
                return new BadRequestObjectResult("Expecting json payload");
            }

            log.LogInformation($"Received: {jsonString}");
            outputQueueItem.Add(jsonString);

            return new CreatedResult("", null);
        }
    }
}
