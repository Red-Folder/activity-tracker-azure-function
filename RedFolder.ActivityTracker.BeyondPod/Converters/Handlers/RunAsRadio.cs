namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class RunAsRadio : CategoryHandler
    {
        private const string CATEGORY = "DevOps";
        private const string FEEDNAME = "RunAs Radio";

        public RunAsRadio() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
