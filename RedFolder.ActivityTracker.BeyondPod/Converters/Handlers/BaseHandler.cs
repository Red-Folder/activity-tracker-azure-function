namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class BaseHandler: IHandler
    {
        public void AddInner(IHandler inner)
        {
            throw new System.NotImplementedException();
        }

        public Models.PodCast Convert(Models.BeyondPod.PodCastTableEntity source)
        {
            return new Models.PodCast
            {
                Created = source.Created,
                FeedName = source.FeedName,
                FeedUrl = source.FeedUrl,
                EpisodeName = source.EpisodeName,
                EpisodeUrl = source.EpisodeUrl,
                EpisodeSummary = source.EpisodeSummary,
                EpisodeDuration = source.EpisodeDuration,
                Category = ""
            };
        }
    }
}
