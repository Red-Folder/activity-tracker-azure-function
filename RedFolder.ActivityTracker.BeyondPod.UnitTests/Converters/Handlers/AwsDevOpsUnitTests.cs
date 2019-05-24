using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class AwsDevOpsUnitTests
    {
        private readonly AwsDevOps _sut;

        private readonly PodCastTableEntity _sample;

        public AwsDevOpsUnitTests()
        {
            _sut = new AwsDevOps();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "DevOps on AWS Radio",
                EpisodeName = "Ep. 24 DevOps Culture with Jeff Gallimore",
                EpisodeUrl = "http://feeds.soundcloud.com/stream/610044948-stelligent-ep-24-devops-culture-with-jeff-gallimore.mp3",
                EpisodePostUrl = "https://soundcloud.com/stelligent/ep-24-devops-culture-with-jeff-gallimore"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Azure & AWS", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://soundcloud.com/stelligent/ep-24-devops-culture-with-jeff-gallimore", result.EpisodeUrl);
        }
    }
}
