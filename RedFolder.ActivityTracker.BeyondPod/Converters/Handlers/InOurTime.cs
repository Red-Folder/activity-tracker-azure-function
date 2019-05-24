namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class InOurTime : CategoryHandler
    {
        private const string CATEGORY = "Fun";
        private const string FEEDNAME = "In Our Time: Science";

        public InOurTime() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
