using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class AzurePodcastUnitTests
    {
        private readonly AzurePodcast _sut;

        private readonly PodCastTableEntity _sample;

        public AzurePodcastUnitTests()
        {
            _sut = new AzurePodcast();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "The Azure Podcast",
                EpisodeName = "Episode 275 - Azure Foundations",
                EpisodeUrl = "http://feedproxy.google.com/~r/TheAzurePodcast/~5/QIlG9h6yfv4/Episode275.mp3",
                EpisodePostUrl = "http://azpodcast.azurewebsites.net/post/Episode-275-Azure-Foundations"
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

            Assert.Equal("http://azpodcast.azurewebsites.net/post/Episode-275-Azure-Foundations", result.EpisodeUrl);
        }
    }
}
