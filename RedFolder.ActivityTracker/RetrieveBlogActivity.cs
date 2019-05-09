using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.Models;
using RedFolder.ActivityTracker.Services;

namespace RedFolder.ActivityTracker
{
    public static class RetrieveBlogActivity
    {
        [FunctionName("RetrieveBlogActivity")]
        public static async Task<BlogActivity> RunAsync([ActivityTrigger] DurableActivityContext context, ILogger log)
        {
            var week = context.GetInput<Week>();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var blogUrl = Environment.GetEnvironmentVariable("BlogUrl", EnvironmentVariableTarget.Process);

            var proxy = new BlogProxy(log);

            log.LogInformation($"Retrieving Blog Activity for ${week.Start.ToShortDateString()} to ${week.End.ToShortDateString()}");

            return await proxy.GetBlogActivityAsync(blogUrl, week.Start, week.End);
        }

    }
}
