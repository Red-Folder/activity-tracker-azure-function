namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class FridayNightComedy : CategoryHandler
    {
        private const string CATEGORY = "Fun";
        private const string FEEDNAME = "Friday Night Comedy from BBC Radio 4";

        public FridayNightComedy() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
