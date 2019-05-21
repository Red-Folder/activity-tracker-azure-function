using RedFolder.ActivityTracker.Models.BeyondPod;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class TroyHunt : CategoryHandler
    {
        private const string CATEGORY = "Security";
        private const string FEEDNAME = "Troy Hunt's Weekly Update Podcast";

        public TroyHunt() : base(CATEGORY, FEEDNAME)
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
