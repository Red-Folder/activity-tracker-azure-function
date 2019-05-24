using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class InfoQCultureUnitTests
    {
        private readonly InfoQCulture _sut;

        private readonly PodCastTableEntity _sample;

        public InfoQCultureUnitTests()
        {
            _sut = new InfoQCulture();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Engineering Culture by InfoQ",
                EpisodeName = "Dave Snowden on Liminality in Cynefin and Moving Beyond Agile to Agility",
                EpisodeUrl = "http://dts.podtrac.com/redirect.mp3/feeds.soundcloud.com/stream/620172810-infoq-engineering-culture-dave-snowden-on-liminality-in-cynefin-and-moving-beyond-agile-to-agility.mp3",
                EpisodePostUrl = "https://soundcloud.com/infoq-engineering-culture/dave-snowden-on-liminality-in-cynefin-and-moving-beyond-agile-to-agility"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Leadership", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://soundcloud.com/infoq-engineering-culture/dave-snowden-on-liminality-in-cynefin-and-moving-beyond-agile-to-agility", result.EpisodeUrl);
        }
    }
}
