using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class WeeklyDevTipsUnitTests
    {
        private readonly WeeklyDevTips _sut;

        private readonly PodCastTableEntity _sample;

        public WeeklyDevTipsUnitTests()
        {
            _sut = new WeeklyDevTips();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Weekly Dev Tips",
                EpisodeName = "Introducing SOLID Principles",
                EpisodeUrl = "https://audio.simplecast.com/416f001e.mp3",
                EpisodePostUrl = "http://www.weeklydevtips.com/047"
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

            Assert.Equal("http://www.weeklydevtips.com/047", result.EpisodeUrl);
        }
    }
}
