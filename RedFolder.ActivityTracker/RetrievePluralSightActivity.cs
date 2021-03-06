using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.Models;
using RedFolder.ActivityTracker.Services;

namespace RedFolder.ActivityTracker
{
    public static class RetrievePluralsightActivity
    {
        [FunctionName("RetrievePluralsightActivity")]
        public static async Task<PluralsightActivity> RunAsync([ActivityTrigger] IDurableActivityContext context, ILogger log)
        {
            var week = context.GetInput<Week>();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var pluralsightId = Environment.GetEnvironmentVariable("PluralsightId", EnvironmentVariableTarget.Process);

            var proxy = new PluralsightProxy(log);

            log.LogInformation($"Retrieving data for ${week.Start.ToShortDateString()} to ${week.End.ToShortDateString()}");
            await proxy.PopulateAsync(pluralsightId, week.Start, week.End);

            log.LogInformation($"Return the Pluralsight Activity");
            return proxy.GetPluralsightActivity();
        }
    }
}
