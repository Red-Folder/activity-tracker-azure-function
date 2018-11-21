using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class ManuallyTriggerWeeklyActivity
    {
        [FunctionName("ManuallyTriggerWeeklyActivity")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            ILogger log)
        {
            await starter.StartNewAsync("OrchestrateWeeklyActivity", null);

            return new OkResult();
        }
    }
}
