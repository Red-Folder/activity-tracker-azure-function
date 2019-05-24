using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class ReactRoundUpUnitTests
    {
        private readonly ReactRoundup _sut;

        private readonly PodCastTableEntity _sample;

        public ReactRoundUpUnitTests()
        {
            _sut = new ReactRoundup();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "React Round Up",
                EpisodeName = "RRU 062: Image Lazy Loading in React",
                EpisodeUrl = "https://media.devchat.tv/reactroundup/RRU_062_Image_Lazy_Loading_in_React.mp3",
                EpisodePostUrl = "https://devchat.tv/react-round-up/rru-062-image-lazy-loading-in-react/"
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

            Assert.Equal("https://devchat.tv/react-round-up/rru-062-image-lazy-loading-in-react/", result.EpisodeUrl);
        }
    }
}
