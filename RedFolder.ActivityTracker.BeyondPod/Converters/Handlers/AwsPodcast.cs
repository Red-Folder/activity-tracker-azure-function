namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class AwsPodcast : CategoryHandler
    {
        private const string CATEGORY = "Azure & AWS";
        private const string FEEDNAME = "AWS Podcast";

        public AwsPodcast() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
