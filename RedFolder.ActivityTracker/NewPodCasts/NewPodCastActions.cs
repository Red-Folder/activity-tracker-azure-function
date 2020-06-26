using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.Models;
using RedFolder.ActivityTracker.Models.ActivityBot;
using RedFolder.ActivityTracker.Services;

namespace RedFolder.ActivityTracker.NewPodCasts
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

            // Disable tweeting of Podcast listens
            //if (podCastAge < 3)
            //{
            //    toBeTweeted.Add(newPodCast);
            //}

            toBeAddedToWeeklyActivity.Add(newPodCast);

            try
            {
                BroadcastToActivityBot(newPodCast);
            }
            catch (Exception ex)
            {
                log.LogError("Failed to Broadcast to ActivityBot", ex);
            }
        }

        private static void BroadcastToActivityBot(PodCast podcast)
        {
            var payload = new Payload
            {
                Type = PayloadType.NewPodCast,
                Contents = podcast
            };

            var secret = Environment.GetEnvironmentVariable("ActivityBotSecret", EnvironmentVariableTarget.Process);

            var client = new ActivityBotProxy(secret);
            client.Broadcast(payload).Wait();
        }
    }
}
