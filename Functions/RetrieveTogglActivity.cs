using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Services;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class RetrieveTogglActivity
    {
        [FunctionName("RetrieveTogglActivity")]
        public static async Task<TogglActivities> RunAsync([ActivityTrigger] DurableActivityContext context, ILogger log)
        {
            var week = context.GetInput<Week>();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var togglApiKey = Environment.GetEnvironmentVariable("TogglApiKey", EnvironmentVariableTarget.Process);
            var togglWorkspaceId = Environment.GetEnvironmentVariable("TogglWorkspaceId", EnvironmentVariableTarget.Process);

            var proxy = new TogglProxy(log);

            log.LogInformation($"Retrieving data for ${week.Start.ToShortDateString()} to ${week.End.ToShortDateString()}");
            await proxy.PopulateAsync(togglApiKey, togglWorkspaceId, week.Start, week.End);

            log.LogInformation($"Get the Skills activities");
            var skillActivity = proxy.GetSkillsActivity();

            log.LogInformation($"Get the Client split");
            var clientActivity = proxy.GetClientActivity();

            log.LogInformation($"Get the focus split");
            var focusActivity = proxy.GetFocusActivity();

            return new TogglActivities
            {
                Skills = skillActivity,
                Clients = clientActivity,
                Focus = focusActivity
            };
        }

    }
}
