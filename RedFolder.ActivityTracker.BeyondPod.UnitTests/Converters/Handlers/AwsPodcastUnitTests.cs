using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class AwsPodcastUnitTests
    {
        private readonly AwsPodcast _sut;

        private readonly PodCastTableEntity _sample;

        public AwsPodcastUnitTests()
        {
            _sut = new AwsPodcast();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "AWS Podcast",
                EpisodeName = "#313: WhereML - Using ML to Detect Locations via Twitter",
                EpisodeUrl = "https://d1le29qyzha1u4.cloudfront.net/AWS_Podcast_Episode_313.mp3",
                EpisodePostUrl = "https://aws.amazon.com/podcasts/aws-podcast/#313"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Azure & AWS", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://aws.amazon.com/podcasts/aws-podcast/#313", result.EpisodeUrl);
        }
    }
}
