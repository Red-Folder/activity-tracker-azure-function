using System.Text.RegularExpressions;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class DarknetDiaries: CategoryHandler
    {
        private const string CATEGORY = "Security";
        private const string FEEDNAME = "Darknet Diaries";

        public DarknetDiaries() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            var episodeNumber = GetEpisodeNumber(source.EpisodeName);
            if (!string.IsNullOrEmpty(episodeNumber))
            {
                destination.EpisodeUrl = $"https://darknetdiaries.com/episode/{episodeNumber}/";
            }
        }

        private string GetEpisodeNumber(string episodeName)
        {
            if (string.IsNullOrEmpty(episodeName)) return null;

            var regex = new Regex("^Ep (\\d*):");

            var match = regex.Match(episodeName);

            if (match.Success && match.Groups.Count >= 2)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
    }
}
