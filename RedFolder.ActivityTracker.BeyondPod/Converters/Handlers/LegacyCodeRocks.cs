using System;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class LegacyCodeRocks : CategoryHandler
    {
        private const string CATEGORY = "General Development";
        private const string FEEDNAME = "Legacy Code Rocks";

        public LegacyCodeRocks() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            var url = source.EpisodePostUrl;

            if (!string.IsNullOrEmpty(url) && url.StartsWith("http:", StringComparison.CurrentCultureIgnoreCase))
            {
                url = $"https:{url.Substring(5)}";
            }

            destination.EpisodeUrl = url;
        }
    }
}
