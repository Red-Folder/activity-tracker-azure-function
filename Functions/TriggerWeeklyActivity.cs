using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class TriggerWeeklyActivity
    {
        [FunctionName("TriggerWeeklyActivity")]
        public static Task Run([TimerTrigger("0 0 10 * * 1")]TimerInfo myTimer, [OrchestrationClient] DurableOrchestrationClient starter, ILogger log)
        {
            var instructions = new OrchestrationInstruction
            {
                StartFrom = OrchestrationStep.FromStart
            };
            return starter.StartNewAsync("OrchestrateWeeklyActivity", instructions);
        }
    }
}
