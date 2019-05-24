using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class FridayNightComedyUnitTests
    {
        private readonly FridayNightComedy _sut;

        private readonly PodCastTableEntity _sample;

        public FridayNightComedyUnitTests()
        {
            _sut = new FridayNightComedy();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Friday Night Comedy from BBC Radio 4",
                EpisodeName = "The News Quiz 17 May 2019",
                EpisodeUrl = "http://open.live.bbc.co.uk/mediaselector/6/redir/version/2.0/mediaset/audio-nondrm-download/proto/http/vpid/p079jvf9.mp3",
                EpisodePostUrl = "http://www.bbc.co.uk/programmes/p079jwb4"
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

            Assert.Equal("http://www.bbc.co.uk/programmes/p079jwb4", result.EpisodeUrl);
        }
    }
}
