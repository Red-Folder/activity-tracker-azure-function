namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class HanselMinutes : CategoryHandler
    {
        private const string CATEGORY = "General Development";
        private const string FEEDNAME = "Hanselminutes";

        public HanselMinutes() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = "https://www.hanselminutes.com/";
        }
    }
}
