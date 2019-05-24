namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class ReactPodcast : CategoryHandler
    {
        private const string CATEGORY = "React & Redux";
        private const string FEEDNAME = "The React Podcast";

        public ReactPodcast() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
