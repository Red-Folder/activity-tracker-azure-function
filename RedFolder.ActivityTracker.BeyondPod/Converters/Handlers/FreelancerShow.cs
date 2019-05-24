namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class FreelancerShow : CategoryHandler
    {
        private const string CATEGORY = "Other";
        private const string FEEDNAME = "The Freelancers' Show";

        public FreelancerShow() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
