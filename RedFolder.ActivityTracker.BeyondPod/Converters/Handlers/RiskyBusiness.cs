﻿namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class RiskyBusiness : CategoryHandler
    {
        private const string CATEGORY = "Security";
        private const string FEEDNAME = "Risky Business";

        public RiskyBusiness() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
