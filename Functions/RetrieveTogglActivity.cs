using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models.Toggl;
using Red_Folder.ActivityTracker.Services;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class RetrieveTogglActivity
    {
        [FunctionName("RetrieveTogglActivity")]
//        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 0 10 * * 1")]TimerInfo myTimer, ILogger log)
        public static async System.Threading.Tasks.Task RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var togglApiKey = Environment.GetEnvironmentVariable("TogglApiKey", EnvironmentVariableTarget.Process);
            var togglWorkspaceId = Environment.GetEnvironmentVariable("TogglWorkspaceId", EnvironmentVariableTarget.Process);

            var start = new DateTime(2018, 09, 10);
            var end = new DateTime(2018, 09, 16);

            var proxy = new TogglProxy(log);

            await proxy.PopulateAsync(togglApiKey, togglWorkspaceId, start, end);

            var skillActivity = proxy.GetSkillsActivity();

            foreach (var skill in skillActivity.Skills)
            {
                log.LogInformation($"{skill.Name}: {skill.TotalDuration}");
            }
        }

    }
}
