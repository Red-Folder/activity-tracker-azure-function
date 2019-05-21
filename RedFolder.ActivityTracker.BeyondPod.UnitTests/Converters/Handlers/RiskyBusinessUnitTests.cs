using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class RiskyBusinessUnitTests
    {
        private readonly RiskyBusiness _sut;

        private readonly PodCastTableEntity _sample;

        public RiskyBusinessUnitTests()
        {
            _sut = new RiskyBusiness();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Risky Business",
                EpisodeName = "Snake Oilers 9 part 2: Rapid7 talks SOAR, Trend Micro on its API-based email security play",
                EpisodeUrl = "http://media2.risky.biz/snakeoilers9pt2.mp3",
                EpisodePostUrl = "https://risky.biz/snakeoilers9pt2"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Security", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://risky.biz/snakeoilers9pt2", result.EpisodeUrl);
        }
    }
}
