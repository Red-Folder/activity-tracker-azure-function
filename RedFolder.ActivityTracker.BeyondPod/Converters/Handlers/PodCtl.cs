namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class PodCtl : CategoryHandler
    {
        private const string CATEGORY = "Containers";
        private const string FEEDNAME = "PodCTL - Containers | Kubernetes | OpenShift";

        public PodCtl() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
