using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class TestWeeklyActivity
    {
        private const string APPROVAL_EVENT = "Approval";

        [FunctionName("TestWeeklyActivity")]
        public async static Task Run([OrchestrationTrigger] DurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var week = Week.FromYearAndWeekNumber(2018, 50);
            var filename = "activity-weekly/2018/50.png";

            var approvalRequest = new ApprovalTableEntity(APPROVAL_EVENT, context.InstanceId);
            approvalRequest.Expires = context.CurrentUtcDateTime.AddDays(1);
            approvalRequest.ImageUrl = $"https://content.red-folder.com/{filename}";

            await context.CallActivityAsync("SendApprovalRequest", approvalRequest);    

            using (var cancellationToken = new CancellationTokenSource())
            {
                var timeoutTask = context.CreateTimer(approvalRequest.Expires, cancellationToken.Token);

                var approvalTask = context.WaitForExternalEvent<bool>(APPROVAL_EVENT);

                var winner = await Task.WhenAny(approvalTask, timeoutTask);

                if (!timeoutTask.IsCompleted)
                {
                    cancellationToken.Cancel();
                }

                if (winner == approvalTask)
                {
                    if (approvalTask.Result)
                    {
                        // approval granted - do the approved action
                    }
                    else
                    {
                        // approval denied - send a notification
                    }
                }
            }

            await context.CallActivityAsync("CleanupApprovalRequest", approvalRequest);

            log.LogInformation($"Run complete");
        }
    }
}
