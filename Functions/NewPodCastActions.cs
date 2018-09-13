using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class NewPodCastActions
    {
        [FunctionName("NewPodCastActions")]
        public static void Run(
            [QueueTrigger("new-podcast", Connection = "AzureWebJobsStorage")]PodCast newPodCast,
            [Queue("podcast-to-be-tweeted", Connection = "AzureWebJobsStorage")]ICollector<PodCast> toBeTweeted,
            [Queue("podcast-to-be-added-to-weekly-activity", Connection = "AzureWebJobsStorage")]ICollector<PodCast> toBeAddedToWeeklyActivity,
            ILogger log)
        {
            var podCastAge = (DateTime.Now - newPodCast.Created).TotalHours;

            if (podCastAge < 3)
            {
                toBeTweeted.Add(newPodCast);
            }

            toBeAddedToWeeklyActivity.Add(newPodCast);
        }
    }
}
