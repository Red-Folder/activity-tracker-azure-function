using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RedFolder.ActivityTracker
{
    public static class Ping
    {
        [FunctionName("Ping")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, 
                                        ILogger log)
        {
            log.LogInformation("Ping called");

            return new OkResult(); ;
        }
    }
}
