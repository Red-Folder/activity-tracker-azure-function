namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class AwsTechChat : CategoryHandler
    {
        private const string CATEGORY = "Azure & AWS";
        private const string FEEDNAME = "AWS TechChat";

        public AwsTechChat() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
