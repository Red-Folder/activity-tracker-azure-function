namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class AdventuresInAngular : CategoryHandler
    {
        private const string CATEGORY = "Angular";
        private const string FEEDNAME = "Adventures in Angular Only";

        public AdventuresInAngular() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.FeedName = "Adventures in Angular";
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
