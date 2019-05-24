using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class HanselMinutesUnitTests
    {
        private readonly HanselMinutes _sut;

        private readonly PodCastTableEntity _sample;

        public HanselMinutesUnitTests()
        {
            _sut = new HanselMinutes();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Hanselminutes",
                EpisodeName = "The Problem with Software by Adam Barr",
                EpisodeUrl = "https://dts.podtrac.com/redirect.mp3/audio.simplecast.com/0750f4e6.mp3",
                EpisodePostUrl = ""
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

            Assert.Equal("https://www.hanselminutes.com/", result.EpisodeUrl);
        }
    }
}
