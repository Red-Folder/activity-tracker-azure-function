using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.Models;

namespace RedFolder.ActivityTracker
{
    public static class TriggerWeeklyActivity
    {
        [FunctionName("TriggerWeeklyActivity")]
        public static Task Run([TimerTrigger("0 0 10 * * 1")]TimerInfo myTimer, [DurableClientAttribute] IDurableOrchestrationClient starter, ILogger log)
        {
            var instructions = new OrchestrationInstruction
            {
                StartFrom = OrchestrationStep.FromStart
            };
            return starter.StartNewAsync("OrchestrateWeeklyActivity", instructions);
        }
    }
}
