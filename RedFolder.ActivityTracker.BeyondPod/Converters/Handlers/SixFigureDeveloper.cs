namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class SixFigureDeveloper : CategoryHandler
    {
        private const string CATEGORY = "General Development";
        private const string FEEDNAME = "The 6 Figure Developer Podcast";

        public SixFigureDeveloper() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
