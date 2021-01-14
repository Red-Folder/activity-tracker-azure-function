using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.Models;
using System;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker
{
    public class CleanupApprovalRequest
    {
        [FunctionName("CleanupApprovalRequest")]
        public static async Task RunAsync([ActivityTrigger] IDurableActivityContext context,
            [Table("PendingApprovals", Connection = "AzureWebJobsStorage")]CloudTable destination,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var approval = context.GetInput<ApprovalTableEntity>();
            approval.ETag = "*";

            log.LogInformation("Deleting pending approval from storage");
            var deleteOperation = TableOperation.Delete(approval);
            await destination.ExecuteAsync(deleteOperation);
        }
    }
}
