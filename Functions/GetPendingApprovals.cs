using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class GetPendingApprovals
    {
        [FunctionName("GetPendingApprovals")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Table("PendingApprovals", Connection = "AzureWebJobsStorage")]CloudTable destination,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            TableContinuationToken token = null;
            var pendingEntities = await destination.ExecuteQuerySegmentedAsync(new TableQuery<ApprovalTableEntity>(), token);

            var pending = pendingEntities.Select(x => new
            {
                eventName = x.EventName,
                instanceId = x.InstanceId,
                imageUrl = x.ImageUrl,
                expires = x.Expires
            }).ToArray();

            return new OkObjectResult(pending);
        }
    }
}
