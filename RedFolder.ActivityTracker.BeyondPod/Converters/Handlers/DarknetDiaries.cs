using RedFolder.ActivityTracker.Models.BeyondPod;
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

        public override Models.PodCast Convert(PodCastTableEntity source)
        {
            var result = base.Convert(source);
       
            var episodeNumber = GetEpisodeNumber(source.EpisodeName);
            if (!string.IsNullOrEmpty(episodeNumber))
            {
                result.EpisodeUrl = $"https://darknetdiaries.com/episode/{episodeNumber}/";
            }

            return result;
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
