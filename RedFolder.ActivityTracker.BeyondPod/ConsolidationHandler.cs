using Microsoft.Extensions.Logging;
using RedFolder.ActivityTracker.BeyondPod.Converters;
using RedFolder.ActivityTracker.BeyondPod.Infrastructure;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.BeyondPod
{
    public class ConsolidationHandler
    {
        private readonly IPodCastConverter _podCastConverter;

        public ConsolidationHandler(IPodCastConverter podCastConverter)
        {
            _podCastConverter = podCastConverter;
        }

        public async Task Process(Models.PodCast source,
                                  IConsolidationRepository consolidationRepository,
                                  INewPodCastQueue newPodCastQueue,
                                  ILogger log)
        {
            log.LogInformation($"Processing Beyond Pod data");

            var existing = await consolidationRepository.Get(source.FeedName, source.EpisodeName);

            var isNew = (existing == null);

            Models.BeyondPod.PodCastTableEntity podCast = isNew ? NewPodCast(source) : UpdatePodCast(existing, source);

            podCast = ApplyFixes(podCast);

            var shouldBeProgressed = ShouldPodCastBeProgressed(podCast);

            if (shouldBeProgressed)
            {
                podCast.Actioned = true;
            }

            // Make sure we can save the podcast before we progress
            await consolidationRepository.Save(podCast);

            if (shouldBeProgressed)
            {
                var activityPodcast = _podCastConverter.Convert(podCast);
                newPodCastQueue.Add(activityPodcast);
            }
        }

        public static Models.BeyondPod.PodCastTableEntity NewPodCast(Models.PodCast source)
        {
            var podCast = new Models.BeyondPod.PodCastTableEntity(source.FeedName, source.EpisodeName);

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

        public static Models.BeyondPod.PodCastTableEntity UpdatePodCast(Models.BeyondPod.PodCastTableEntity podCast, Models.PodCast source)
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

        public static Models.BeyondPod.PodCastTableEntity ApplyFixes(Models.BeyondPod.PodCastTableEntity podCast)
        {
            // Apply fix for when duration is less than position
            if (podCast.EpisodeDuration < podCast.EpisodePosition) podCast.EpisodeDuration = podCast.EpisodePosition;

            if (podCast.FeedName == "JavaScript Jabber Only") podCast.FeedName = "JavaScript Jabber";
            if (podCast.FeedName == "Adventures in Angular Only") podCast.FeedName = "Adventures in Angular";

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
    }
}
