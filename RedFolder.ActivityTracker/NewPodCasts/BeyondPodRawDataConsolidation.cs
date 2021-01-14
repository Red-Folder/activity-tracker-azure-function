using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.BeyondPod;
using RedFolder.ActivityTracker.BeyondPod.Infrastructure;
using RedFolder.ActivityTracker.Models;
using System;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.NewPodCasts
{
    public class BeyondPodRawDataConsolidation
    {
        private readonly ConsolidationHandler _handler;

        public BeyondPodRawDataConsolidation(ConsolidationHandler handler)
        {
            _handler = handler;
        }

        [FunctionName("BeyondPodRawDataConsolidation")]
        public async Task RunAsync(
                                [QueueTrigger("beyond-pod-raw-data", Connection= "AzureWebJobsStorage")]Models.BeyondPod.PodCast source, 
                                [Table("PodCasts", Connection = "AzureWebJobsStorage")]CloudTable destination, 
                                [Queue("new-podcast", Connection = "AzureWebJobsStorage")]ICollector<PodCast> podcastReadyToGo,
                                ILogger log)
        {
            log.LogInformation("BeyondPodRawDataConsolidation: Processing Beyond Pod data");

            try
            {
                var consolidationRepository = new ConsolidationRepository(destination);
                var newPodCastQueue = new NewPodCastQueue(podcastReadyToGo);

                await _handler.Process(source, consolidationRepository, newPodCastQueue, log);
            }
            catch (Exception ex)
            {
                log.LogError("BeyondPodRawDataConsolidation failed", ex);
            }

            log.LogInformation("BeyondPodRawDataConsolidation: Processing complete");
        }
    }
}
