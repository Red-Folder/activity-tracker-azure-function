namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class InfoQ : CategoryHandler
    {
        private const string CATEGORY = "General Development";
        private const string FEEDNAME = "The InfoQ Podcast";

        public InfoQ() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
