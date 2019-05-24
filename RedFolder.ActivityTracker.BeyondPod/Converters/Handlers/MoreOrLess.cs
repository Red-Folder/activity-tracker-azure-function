namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class MoreOrLess : CategoryHandler
    {
        private const string CATEGORY = "Fun";
        private const string FEEDNAME = "More or Less: Behind the Stats";

        public MoreOrLess() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
