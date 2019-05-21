using RedFolder.ActivityTracker.Models.BeyondPod;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class RiskyBusiness : CategoryHandler
    {
        private const string CATEGORY = "Security";
        private const string FEEDNAME = "Risky Business";

        public RiskyBusiness() : base(CATEGORY, FEEDNAME)
        {
        }

        public override Models.PodCast Convert(PodCastTableEntity source)
        {
            var result =  base.Convert(source);

            result.EpisodeUrl = source.EpisodePostUrl;

            return result;
        }
    }
}
