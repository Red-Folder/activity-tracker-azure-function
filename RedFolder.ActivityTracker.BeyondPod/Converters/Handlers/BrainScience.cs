namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class BrainScience : CategoryHandler
    {
        private const string CATEGORY = "Leadership";
        private const string FEEDNAME = "Brain Science";

        public BrainScience() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
