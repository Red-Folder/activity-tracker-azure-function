namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class WeeklyDevTips : CategoryHandler
    {
        private const string CATEGORY = "General Development";
        private const string FEEDNAME = "Weekly Dev Tips";

        public WeeklyDevTips() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
