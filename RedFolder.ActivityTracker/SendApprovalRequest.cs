using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedFolder.ActivityTracker.Models;
using RedFolder.ActivityTracker.Models.ActivityBot;
using RedFolder.ActivityTracker.Models.Notifications;
using RedFolder.ActivityTracker.Services;

namespace RedFolder.ActivityTracker
{
    public static class SendApprovalRequest
    {
        [FunctionName("SendApprovalRequest")]
        public static async Task Run([ActivityTrigger] IDurableActivityContext context,
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

            var notification = new FcmPayload
            {
                Payload = new FcmPayload.Notification
                {
                    Title = "Awaiting Approval",
                    Message = "New Activity Image",
                    ClickAction = "http://localhost:3000/",
                    IconUrl = approval.ImageUrl
                }
            };
            await hub.SendFcmNativeNotificationAsync(JsonConvert.SerializeObject(notification));

            // And also alert on Slack
            var payload = new Payload
            {
                Type = PayloadType.WeekylActivity,
                Contents = approval
            };

            var secret = Environment.GetEnvironmentVariable("ActivityBotSecret", EnvironmentVariableTarget.Process);

            var client = new ActivityBotProxy(secret);
            client.Broadcast(payload).Wait();
        }
    }
}
