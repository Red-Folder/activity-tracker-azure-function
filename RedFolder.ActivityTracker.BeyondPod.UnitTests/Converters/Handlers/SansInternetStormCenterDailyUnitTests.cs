using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class SansInternetStormCenterDailyUnitTests
    {
        private readonly SansInternetStormCenterDaily _sut;

        private readonly PodCastTableEntity _sample;

        public SansInternetStormCenterDailyUnitTests()
        {
            _sut = new SansInternetStormCenterDaily();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "SANS Internet Storm Center Daily Network Cyber Security and Information Security Podcast",
                EpisodeName = "ISC StormCast for Tuesday, April 23rd 2019",
                EpisodeUrl = "https://isc.sans.edu/podcast/podcast6466.mp3",
                EpisodePostUrl = "https://isc.sans.edu/podcastdetail.html?id=6466"
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

            Assert.Equal("https://isc.sans.edu/podcastdetail.html?id=6466", result.EpisodeUrl);
        }
    }
}
