namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class InfiniteMonkeyCage : CategoryHandler
    {
        private const string CATEGORY = "Fun";
        private const string FEEDNAME = "The Infinite Monkey Cage";

        public InfiniteMonkeyCage() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
