using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class InfiniteMonkeyCageUnitTests
    {
        private readonly InfiniteMonkeyCage _sut;

        private readonly PodCastTableEntity _sample;

        public InfiniteMonkeyCageUnitTests()
        {
            _sut = new InfiniteMonkeyCage();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "The Infinite Monkey Cage",
                EpisodeName = "How We Measure the Universe",
                EpisodeUrl = "http://open.live.bbc.co.uk/mediaselector/5/redir/version/2.0/mediaset/audio-nondrm-download/proto/http/vpid/p070kt20.mp3",
                EpisodePostUrl = "http://www.bbc.co.uk/programmes/m0002g8k"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Fun", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("http://www.bbc.co.uk/programmes/m0002g8k", result.EpisodeUrl);
        }
    }
}
