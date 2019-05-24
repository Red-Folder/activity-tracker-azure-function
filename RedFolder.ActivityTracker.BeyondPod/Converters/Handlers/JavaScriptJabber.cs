namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class JavaScriptJabber : CategoryHandler
    {
        private const string CATEGORY = "JavaScript";
        private const string FEEDNAME = "JavaScript Jabber Only";

        public JavaScriptJabber() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.FeedName = "JavaScript Jabber";
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
