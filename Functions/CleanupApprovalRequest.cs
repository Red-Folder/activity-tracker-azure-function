using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Red_Folder.ActivityTracker.Models;
using System;
using System.Threading.Tasks;

namespace Red_Folder.ActivityTracker.Functions
{
    public class CleanupApprovalRequest
    {
        [FunctionName("CleanupApprovalRequest")]
        public static async Task RunAsync([ActivityTrigger] DurableActivityContext context,
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
