using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class ReactPodcastUnitTests
    {
        private readonly ReactPodcast _sut;

        private readonly PodCastTableEntity _sample;

        public ReactPodcastUnitTests()
        {
            _sut = new ReactPodcast();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "The React Podcast",
                EpisodeName = "44: Create Value for Others with Nader Dabit. On podcasting, speaking, mobile devrel at AWS Amplify, AppSync for simple GraphQL servers, and his new book React Native in Action.",
                EpisodeUrl = "https://audio.simplecast.com/bb7f1c9a.mp3",
                EpisodePostUrl = "http://reactpodcast.com/44"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("React & Redux", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("http://reactpodcast.com/44", result.EpisodeUrl);
        }
    }
}
