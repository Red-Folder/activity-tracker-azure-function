namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class AzureDevOps : CategoryHandler
    {
        private const string CATEGORY = "Azure & AWS";
        private const string FEEDNAME = "Azure DevOps Podcast";

        public AzureDevOps() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
