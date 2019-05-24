namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class SoftwareEngineeringRadio : CategoryHandler
    {
        private const string CATEGORY = "General Development";
        private const string FEEDNAME = "Software Engineering Radio - The Podcast for Professional Software Developers";

        public SoftwareEngineeringRadio() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
