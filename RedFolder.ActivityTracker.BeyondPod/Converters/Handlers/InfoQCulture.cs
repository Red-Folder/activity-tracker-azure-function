namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class InfoQCulture : CategoryHandler
    {
        private const string CATEGORY = "Leadership";
        private const string FEEDNAME = "Engineering Culture by InfoQ";

        public InfoQCulture() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
