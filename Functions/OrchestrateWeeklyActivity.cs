using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class OrchestrateWeeklyActivity
    {
        [FunctionName("OrchestrateWeeklyActivity")]
        public async static Task Run([OrchestrationTrigger] DurableOrchestrationContext context, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var week = await context.CallActivityAsync<Week>("GetWeek", null);
            log.LogInformation($"Running for week {week.WeekNumber}");


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

            var imageData = await context.CallActivityAsync<byte[]>("CaptureActivityImage", week);

            var activityImage = new ActivityImage
            {
                Week = week,
                ImageData = imageData
            };
            await context.CallActivityAsync("WriteActivityImage", activityImage);

            log.LogInformation($"Run complete");
        }
    }
}
