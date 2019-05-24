using RedFolder.ActivityTracker.Models.BeyondPod;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class HerdingCode : CategoryHandler
    {
        private const string CATEGORY = "General Development";
        private const string FEEDNAME = "Herding Code";

        public HerdingCode() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
