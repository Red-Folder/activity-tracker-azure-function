﻿namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class TroyHunt : CategoryHandler
    {
        private const string CATEGORY = "Security";
        private const string FEEDNAME = "Troy Hunt's Weekly Update Podcast";

        public TroyHunt() : base(CATEGORY, FEEDNAME)
        {
        }

        protected override void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
            destination.EpisodeUrl = source.EpisodePostUrl;
        }
    }
}
