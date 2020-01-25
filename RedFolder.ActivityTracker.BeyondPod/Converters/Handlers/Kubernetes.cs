namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class Kubernetes : CategoryHandler
    {
        private const string CATEGORY = "DevOps";
        private const string FEEDNAME = "Kubernetes Podcast from Google";

        public Kubernetes() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
