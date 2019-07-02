using System.Text.RegularExpressions;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class BetterROI : CategoryHandler
    {
        private const string CATEGORY = "Leadership";
        private const string FEEDNAME = "Better ROI from Software Development";

        private const string ALPHA_LOWER = "abcdefghijklmnopqrstuvwxyz";
        private const string ALPHA_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMERIC = "0123456789";
        private const string SPACE = " ";

        public BetterROI() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = $"https://red-folder.com/podcasts/{MakeSafe(source.EpisodeName)}";
        }

        private string MakeSafe(string source)
        {
            var pattern = string.Concat(ALPHA_LOWER, ALPHA_UPPER, NUMERIC, SPACE);
            var cleaned = Regex.Replace(source, $"[^{pattern}]", "");

            return cleaned.Replace(" ", "-");
        }
    }
}
