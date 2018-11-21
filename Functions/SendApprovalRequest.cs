using System;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Models.Notifications;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class SendApprovalRequest
    {
        [FunctionName("SendApprovalRequest")]
        public static async Task Run([ActivityTrigger] DurableActivityContext context,
            [Table("PendingApprovals", Connection = "AzureWebJobsStorage")]CloudTable destination,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var approval = context.GetInput<ApprovalTableEntity>();

            log.LogInformation("Saving pending approval to storage");
            var insertOperation = TableOperation.Insert(approval);
            await destination.ExecuteAsync(insertOperation);

            log.LogInformation("Sending notification");

            var connectionString = Environment.GetEnvironmentVariable("NotificationHubConnectionString", EnvironmentVariableTarget.Process);
            var hub = new NotificationHubClient(connectionString, "rfc-activity");

            var notification = new GcmPayload
            {
                Payload = new GcmPayload.Notification
                {
                    Title = "Awaiting Approval",
                    Message = "New Activity Image",
                    ClickAction = "http://localhost:3000/",
                    IconUrl = approval.ImageUrl
                }
            };
            await hub.SendGcmNativeNotificationAsync(JsonConvert.SerializeObject(notification));
        }
    }
}
