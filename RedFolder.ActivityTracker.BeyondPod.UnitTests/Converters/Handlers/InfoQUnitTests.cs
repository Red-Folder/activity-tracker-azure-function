using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class InfoQUnitTests
    {
        private readonly InfoQ _sut;

        private readonly PodCastTableEntity _sample;

        public InfoQUnitTests()
        {
            _sut = new InfoQ();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "The InfoQ Podcast",
                EpisodeName = "Ashley Williams on Web Assembly, Wasi, & the Application Edge*",
                EpisodeUrl = "http://dts.podtrac.com/redirect.mp3/feeds.soundcloud.com/stream/611737764-infoq-channel-ashley-williams-on-web-assembly-wasi-the-application-edge.mp3",
                EpisodePostUrl = "https://soundcloud.com/infoq-channel/ashley-williams-on-web-assembly-wasi-the-application-edge"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("General Development", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://soundcloud.com/infoq-channel/ashley-williams-on-web-assembly-wasi-the-application-edge", result.EpisodeUrl);
        }
    }
}
