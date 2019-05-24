using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class DotNetRocksUnitTests
    {
        private readonly DotNetRocks _sut;

        private readonly PodCastTableEntity _sample;

        public DotNetRocksUnitTests()
        {
            _sut = new DotNetRocks();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "NET Rocks",
                EpisodeName = "The Modern Developer with Dan North",
                EpisodeUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~5/kvAnVYq8hzk/26ea2a24183c9f76dbe9d9edf21ce241.mp3",
                EpisodePostUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~3/BsgnmUSSm3U/default.aspx"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal(".Net & C#", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~3/BsgnmUSSm3U/default.aspx", result.EpisodeUrl);
        }
    }
}
