namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class DotNetRocks : CategoryHandler
    {
        private const string CATEGORY = ".Net & C#";
        private const string FEEDNAME = "NET Rocks";

        public DotNetRocks() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
