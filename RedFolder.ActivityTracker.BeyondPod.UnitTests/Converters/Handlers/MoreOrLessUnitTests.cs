using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class MoreOrLessUnitTests
    {
        private readonly MoreOrLess _sut;

        private readonly PodCastTableEntity _sample;

        public MoreOrLessUnitTests()
        {
            _sut = new MoreOrLess();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "More or Less: Behind the Stats",
                EpisodeName = "Making music out of Money",
                EpisodeUrl = "http://open.live.bbc.co.uk/mediaselector/6/redir/version/2.0/mediaset/audio-nondrm-download/proto/http/vpid/p079kkp4.mp3",
                EpisodePostUrl = "http://www.bbc.co.uk/programmes/p079km1v"
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

            Assert.Equal("http://www.bbc.co.uk/programmes/p079km1v", result.EpisodeUrl);
        }
    }
}
