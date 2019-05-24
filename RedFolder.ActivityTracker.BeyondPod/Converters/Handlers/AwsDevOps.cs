namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class AwsDevOps : CategoryHandler
    {
        private const string CATEGORY = "Azure & AWS";
        private const string FEEDNAME = "DevOps on AWS Radio";

        public AwsDevOps() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
