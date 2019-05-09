using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedFolder.ActivityTracker.Models;

namespace RedFolder.ActivityTracker
{
    public static class ManuallyTriggerWeeklyActivity
    {
        [FunctionName("ManuallyTriggerWeeklyActivity")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            ILogger log)
        {
            var json = await req.ReadAsStringAsync();
            var instruction = JsonConvert.DeserializeObject<OrchestrationInstruction>(json);
            await starter.StartNewAsync("OrchestrateWeeklyActivity", instruction);

            return new OkResult();
        }
    }
}
