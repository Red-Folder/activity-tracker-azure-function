using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class NewWeeklyActivityActions
    {
        [FunctionName("NewWeeklyActivityActions")]
        public static void Run(
            [ActivityTrigger] DurableActivityContext context,
            [Queue("activity-to-be-tweeted", Connection = "AzureWebJobsStorage")]ICollector<WeeklyActivityToBeTweeted> toBeTweeted,
            TraceWriter log)
        {
            var newActivity = context.GetInput<WeeklyActivityToBeTweeted>();
            toBeTweeted.Add(newActivity);
        }
    }
}
