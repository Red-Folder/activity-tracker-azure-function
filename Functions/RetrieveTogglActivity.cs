using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

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

            using (var client = new HttpClient())
            {
                var credentials = Encoding.ASCII.GetBytes($"{togglApiKey}:api_token");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));

                var url = BuildUrl(togglWorkspaceId, new DateTime(2018, 09, 08), new DateTime(2018, 09, 15));
                var result = await client.GetAsync(url);
                string resultContent = await result.Content.ReadAsStringAsync();
                log.LogInformation(resultContent);
            }
        }

        private static string BuildUrl(string workspaceId, DateTime from, DateTime to) 
        {
            var builder = new StringBuilder();
            builder.Append("https://toggl.com/reports/api/v2/details?");
            builder.Append($"workspace_id={workspaceId}");
            builder.Append($"&since={from.ToString("yyyy-MM-dd")}");
            builder.Append($"&until={to.ToString("yyyy-MM-dd")}");
            builder.Append("&user_agent=red-folder.com");

            return builder.ToString();
        }
    }
}
