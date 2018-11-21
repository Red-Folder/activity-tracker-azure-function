using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class Approve
    {
        [FunctionName("Approve")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [OrchestrationClient] DurableOrchestrationClient client,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var instanceId = data.instanceId.Value;
            var eventName = data.eventName.Value;
            var approved = data.approved.Value;

            await client.RaiseEventAsync(instanceId, eventName, approved);

            return new OkResult();
        }
    }
}
