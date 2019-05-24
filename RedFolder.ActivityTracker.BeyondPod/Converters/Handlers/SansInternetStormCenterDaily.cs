using System;
using RedFolder.ActivityTracker.Models.BeyondPod;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class SansInternetStormCenterDaily : CategoryHandler
    {
        private const string CATEGORY = "Security";
        private const string FEEDNAME = "SANS Internet Storm Center Daily Network Cyber Security and Information Security Podcast";

        public SansInternetStormCenterDaily() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
