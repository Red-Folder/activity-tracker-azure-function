using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class BetterROIUnitTests
    {
        private readonly BetterROI _sut;

        private readonly PodCastTableEntity _sample;

        public BetterROIUnitTests()
        {
            _sut = new BetterROI();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Better ROI from Software Development",
                EpisodeName = "Episode 0 - Why I'm doing this podcast"
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

            Assert.Equal("https://red-folder.com/podcasts/Episode-0--Why-Im-doing-this-podcast", result.EpisodeUrl);
        }
    }
}
