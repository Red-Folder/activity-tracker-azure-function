using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using RedFolder.ActivityTracker.BeyondPod.Converters;
using RedFolder.ActivityTracker.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.NewPodCasts
{
    public class BeyondPodRawDataConsolidation
    {
        private readonly IPodCastConverter _podCastConverter;

        public BeyondPodRawDataConsolidation(IPodCastConverter podCastConverter)
        {
            _podCastConverter = podCastConverter;
        }

        [FunctionName("BeyondPodRawDataConsolidation")]
        public async Task RunAsync(
                                [QueueTrigger("beyond-pod-raw-data", Connection= "AzureWebJobsStorage")]Models.BeyondPod.PodCast source, 
                                [Table("PodCasts", Connection = "AzureWebJobsStorage")]CloudTable destination, 
                                [Queue("new-podcast", Connection = "AzureWebJobsStorage")]ICollector<PodCast> podcastReadyToGo,
                                ILogger log)
        {
            log.LogInformation($"Processing Beyond Pod data");

            var partitionKey = ToAzureKeyString(source.FeedName);
            var rowKey = ToAzureKeyString(source.EpisodeName);
            var retrieveOperation = TableOperation.Retrieve<Models.BeyondPod.PodCastTableEntity>(partitionKey, rowKey);

            log.LogInformation($"Trying to load existing podcast record");
            var query = await destination.ExecuteAsync(retrieveOperation);
            var isNew = (query.Result == null);

            Models.BeyondPod.PodCastTableEntity podCast = isNew ? NewPodCast(partitionKey, rowKey, source) : UpdatePodCast((Models.BeyondPod.PodCastTableEntity)query.Result, source);

            podCast = ApplyFixes(podCast);

            var shouldBeProgressed = ShouldPodCastBeProgressed(podCast);

            if (shouldBeProgressed)
            {
                podCast.Actioned = true;
            }

            // Make sure we can save the podcast before we progress
            await Save(destination, isNew, podCast);

            if (shouldBeProgressed)
            {
                var activityPodcast = _podCastConverter.Convert(podCast);
                podcastReadyToGo.Add(activityPodcast);
            }
        }

        public static Models.BeyondPod.PodCastTableEntity NewPodCast(String partitionKey, String rowKey, Models.BeyondPod.PodCast source)
        {
            var podCast = new Models.BeyondPod.PodCastTableEntity(partitionKey, rowKey);

            podCast.Created = source.Created;
            podCast.Playing = source.Playing;
            podCast.FeedName = source.FeedName;
            podCast.FeedUrl = source.FeedUrl;
            podCast.EpisodeName = source.EpisodeName;
            podCast.EpisodeUrl = source.EpisodeUrl;
            podCast.EpisodeFile = source.EpisodeFile;
            podCast.EpisodePostUrl = source.EpisodePostUrl;
            podCast.EpisodeMime = source.EpisodeMime;
            podCast.EpisodeSummary = source.EpisodeSummary;
            podCast.EpisodeDuration = source.EpisodeDuration;
            podCast.EpisodePosition = source.EpisodePosition;
            podCast.Artist = source.Artist;
            podCast.Album = source.Album;
            podCast.Track = source.Track;

            return podCast;
        }

        public static Models.BeyondPod.PodCastTableEntity UpdatePodCast(Models.BeyondPod.PodCastTableEntity podCast, Models.BeyondPod.PodCast source)
        {
            if (source.Created > podCast.Created)
            {
                podCast.Created = source.Created;
                podCast.Playing = source.Playing;
                podCast.EpisodePosition = source.EpisodePosition;

                // Sometimes we seem to see zero durations
                if (podCast.EpisodeDuration < source.EpisodeDuration)
                {
                    podCast.EpisodeDuration = source.EpisodeDuration;
                }
            }

            return podCast;
        }

        public static async Task Save(CloudTable destination, bool isNew, Models.BeyondPod.PodCastTableEntity podCast)
        {
            if (isNew)
            {
                var insertOperation = TableOperation.Insert(podCast);
                await destination.ExecuteAsync(insertOperation);
            }
            else
            {
                var replaceOperation = TableOperation.Replace(podCast);
                await destination.ExecuteAsync(replaceOperation);
            }
        }

        public static Models.BeyondPod.PodCastTableEntity ApplyFixes(Models.BeyondPod.PodCastTableEntity podCast)
        {
            // Apply fix for when duration is less than position
            if (podCast.EpisodeDuration < podCast.EpisodePosition) podCast.EpisodeDuration = podCast.EpisodePosition;

            return podCast;
        }

        public static bool ShouldPodCastBeProgressed(Models.BeyondPod.PodCastTableEntity podCast)
        {
            if (podCast.Actioned) return false;
            if (podCast.EpisodeDuration == 0) return false;

            var percentageThrough = (float)podCast.EpisodePosition / (float)podCast.EpisodeDuration;

            if (percentageThrough <= 0.9) return false;

            return true;
        }

        public static string ToAzureKeyString(string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str
                .Where(c => c != '/'
                            && c != '\\'
                            && c != '#'
                            && c != '/'
                            && c != '?'
                            && !char.IsControl(c)))
                sb.Append(c);
            return sb.ToString();
        }
    }
}
