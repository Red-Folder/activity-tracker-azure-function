namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class ReactRoundup : CategoryHandler
    {
        private const string CATEGORY = "React & Redux";
        private const string FEEDNAME = "React Round Up";

        public ReactRoundup() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
