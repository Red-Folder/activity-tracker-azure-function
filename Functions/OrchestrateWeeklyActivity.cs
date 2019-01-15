using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class OrchestrateWeeklyActivity
    {
        private const string APPROVAL_EVENT = "Approval";

        [FunctionName("OrchestrateWeeklyActivity")]
        public async static Task Run([OrchestrationTrigger] DurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var instruction = context.GetInput<OrchestrationInstruction>();

            var week = instruction.PerformGetWeek ?
                            await GetWeek(context) :
                            Week.FromYearAndWeekNumber(instruction.Year, instruction.WeekNumber);


            log.LogInformation($"Running for week {week.WeekNumber}");

            if (instruction.PerformRetrieval)
            {
                await RetrieveActivity(context, week);
            }

            var filename = instruction.PerformScreenCapture ?
                            await ScreenCapture(context, week) :
                            instruction.Filename;

            
            //var week = Week.FromYearAndWeekNumber(2018, 51);
            //var filename = "activity-weekly/2018/51.png";

            var approvalRequest = new ApprovalTableEntity(APPROVAL_EVENT, context.InstanceId);
            approvalRequest.Expires = context.CurrentUtcDateTime.AddDays(1);
            approvalRequest.ImageUrl = $"https://content.red-folder.com/{filename}";
            approvalRequest.WeekNumber = week.WeekNumber;
            approvalRequest.From = week.Start;
            approvalRequest.To = week.End;

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
                        var weeklyActivityToBeTweeted = new WeeklyActivityToBeTweeted
                        {
                            Year = week.Year,
                            WeekNumber = week.WeekNumber,
                            ImageUrl = approvalRequest.ImageUrl
                        };

                        await context.CallActivityAsync("NewWeeklyActivityActions", weeklyActivityToBeTweeted);
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

        public static async Task<Week> GetWeek(DurableOrchestrationContext context)
        {
            return await context.CallActivityAsync<Week>("GetWeek", null);
        }

        private static async Task RetrieveActivity(DurableOrchestrationContext context, Week week)
        {
            var activityToSave = new Models.WeekActivity(week.Year, week.WeekNumber);

            var blogTask = context.CallActivityAsync<BlogActivity>("RetrieveBlogActivity", week);
            var pluralsightTask = context.CallActivityAsync<PluralsightActivity>("RetrievePluralsightActivity", week);
            var togglTask = context.CallActivityAsync<TogglActivities>("RetrieveTogglActivity", week);

            await Task.WhenAll(blogTask, pluralsightTask, togglTask);

            activityToSave.Blogs = blogTask.Result;
            activityToSave.Pluralsight = pluralsightTask.Result;
            activityToSave.Skills = togglTask.Result.Skills;
            activityToSave.Focus = togglTask.Result.Focus;
            activityToSave.Clients = togglTask.Result.Clients;

            await context.CallActivityAsync("SaveActivity", activityToSave);
        }

        private static async Task<string> ScreenCapture(DurableOrchestrationContext context, Week week)
        {
            var imageData = await context.CallActivityAsync<byte[]>("CaptureActivityImage", week);

            var activityImage = new ActivityImage
            {
                Week = week,
                ImageData = imageData
            };
            return await context.CallActivityAsync<string>("WriteActivityImage", activityImage);
        }
    }
}
