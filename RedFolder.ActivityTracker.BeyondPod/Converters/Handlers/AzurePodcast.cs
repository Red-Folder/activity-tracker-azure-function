namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class AzurePodcast : CategoryHandler
    {
        private const string CATEGORY = "Azure & AWS";
        private const string FEEDNAME = "The Azure Podcast";

        public AzurePodcast() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
