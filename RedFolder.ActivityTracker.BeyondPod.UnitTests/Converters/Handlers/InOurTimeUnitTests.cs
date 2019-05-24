using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class InOurTimeUnitTests
    {
        private readonly InOurTime _sut;

        private readonly PodCastTableEntity _sample;

        public InOurTimeUnitTests()
        {
            _sut = new InOurTime();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "In Our Time: Science",
                EpisodeName = "Pheromones",
                EpisodeUrl = "http://open.live.bbc.co.uk/mediaselector/5/redir/version/2.0/mediaset/audio-nondrm-download/proto/http/vpid/p071jzyp.mp3",
                EpisodePostUrl = "http://www.bbc.co.uk/programmes/m0002mdl"
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

            Assert.Equal("http://www.bbc.co.uk/programmes/m0002mdl", result.EpisodeUrl);
        }
    }
}
