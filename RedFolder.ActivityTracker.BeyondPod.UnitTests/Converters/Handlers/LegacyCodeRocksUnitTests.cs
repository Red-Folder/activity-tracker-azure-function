using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class LegacyCodeRocksUnitTests
    {
        private readonly LegacyCodeRocks _sut;

        private readonly PodCastTableEntity _sample;

        public LegacyCodeRocksUnitTests()
        {
            _sut = new LegacyCodeRocks();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Legacy Code Rocks",
                EpisodeName = "Defensive Coding with Edaqa Mortoray",
                EpisodeUrl = "http://traffic.libsyn.com/legacycoderocks/Defensive_Coding_with_Edaqa_Mortoray.mp3?dest-id=383642",
                EpisodePostUrl = "http://legacycoderocks.libsyn.com/defensive-coding-with-edaqa-mortoray"
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

            Assert.Equal("https://legacycoderocks.libsyn.com/defensive-coding-with-edaqa-mortoray", result.EpisodeUrl);
        }
    }
}
