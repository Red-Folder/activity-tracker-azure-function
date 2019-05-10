using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RedFolder.ActivityTracker
{
    public class Ping
    {
        private readonly ITest _test;

        public Ping(ITest test)
        {
            _test = test;
        }

        [FunctionName("Ping")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
                                 ILogger log)
        {
            log.LogInformation("Ping called");

            return new OkObjectResult(_test.Hello());
            //return new OkResult();
        }
    }
}
