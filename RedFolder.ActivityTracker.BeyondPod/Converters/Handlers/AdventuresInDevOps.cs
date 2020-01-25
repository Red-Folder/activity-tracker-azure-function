namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class AdventuresInDevOps : CategoryHandler
    {
        private const string CATEGORY = "DevOps";
        private const string FEEDNAME = "Adventures in DevOps";

        public AdventuresInDevOps() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
