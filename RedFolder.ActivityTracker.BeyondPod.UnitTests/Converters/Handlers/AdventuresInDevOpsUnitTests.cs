using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class AdventuresInDevOpsUnitTests
    {
        private readonly AdventuresInDevOps _sut;

        private readonly PodCastTableEntity _sample;

        public AdventuresInDevOpsUnitTests()
        {
            _sut = new AdventuresInDevOps();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Adventures in DevOps",
                EpisodeName = "DevOps 12: Containerizing an Application",
                EpisodeUrl = "https://media.devchat.tv/adventures-in-devops/ADO_012_Panel.mp3",
                EpisodePostUrl = "https://devchat.tv/adventures-in-devops/devops-12-containerizing-an-application"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("DevOps", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://devchat.tv/adventures-in-devops/devops-12-containerizing-an-application", result.EpisodeUrl);
        }
    }
}
