namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class TwoThousandBooks : CategoryHandler
    {
        private const string CATEGORY = "Leadership";
        private const string FEEDNAME = "2000 Books for Ambitious Entrepreneurs - Author Interviews and Book Summaries";

        public TwoThousandBooks() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
