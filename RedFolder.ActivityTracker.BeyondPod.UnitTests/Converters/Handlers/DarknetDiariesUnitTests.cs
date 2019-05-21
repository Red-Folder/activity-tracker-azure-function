using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class DarknetDiariesUnitTests
    {
        private readonly DarknetDiaries _sut;

        private readonly PodCastTableEntity _sample;

        public DarknetDiariesUnitTests()
        {
            _sut = new DarknetDiaries();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Darknet Diaries",
                EpisodeName = "Ep 38: Dark Caracal",
                EpisodeUrl = "https://www.podtrac.com/pts/redirect.mp3/traffic.megaphone.fm/ADV2564803266.mp3?updated=1557778055",
                EpisodePostUrl = ""
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

            Assert.Equal("https://darknetdiaries.com/episode/38/", result.EpisodeUrl);
        }
    }
}
