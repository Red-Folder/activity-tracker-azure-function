namespace RedFolder.ActivityTracker.Services.PodCast
{
    public class BaseHandler: IHandler
    {
        public void AddInner(IHandler inner)
        {
            throw new System.NotImplementedException();
        }

        public Models.PodCast Convert(Models.PodCastTableEntity source)
        {
            return new Models.PodCast
            {
                Created = source.Created,
                Playing = source.Playing,
                FeedName = source.FeedName,
                FeedUrl = source.FeedUrl,
                EpisodeName = source.EpisodeName,
                EpisodeUrl = source.EpisodeUrl,
                EpisodeFile = source.EpisodeFile,
                EpisodePostUrl = source.EpisodePostUrl,
                EpisodeMime = source.EpisodeMime,
                EpisodeSummary = source.EpisodeSummary,
                EpisodeDuration = source.EpisodeDuration,
                EpisodePosition = source.EpisodePosition,
                Artist = source.Artist,
                Album = source.Album,
                Track = source.Track
            };
        }
    }
}
