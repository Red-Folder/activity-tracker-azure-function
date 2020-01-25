using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class BrainScienceUnitTests
    {
        private readonly BrainScience _sut;

        private readonly PodCastTableEntity _sample;

        public BrainScienceUnitTests()
        {
            _sut = new BrainScience();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Brain Science",
                EpisodeName = "What are you thinking?",
                EpisodeUrl = "https://cdn.changelog.com/uploads/brainscience/7/brain-science-7.mp3",
                EpisodePostUrl = "https://changelog.com/brainscience/7"
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

            Assert.Equal("https://changelog.com/brainscience/7", result.EpisodeUrl);
        }
    }
}
