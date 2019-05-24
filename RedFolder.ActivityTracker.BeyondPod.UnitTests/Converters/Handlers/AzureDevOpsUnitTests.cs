using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class AzureDevOpsUnitTests
    {
        private readonly AzureDevOps _sut;

        private readonly PodCastTableEntity _sample;

        public AzureDevOpsUnitTests()
        {
            _sut = new AzureDevOps();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Azure DevOps Podcast",
                EpisodeName = "Mark Miller on Developer Productivity",
                EpisodeUrl = "http://traffic.libsyn.com/azuredevops/ADP_037-2.mp3?dest-id=768873",
                EpisodePostUrl = "http://azuredevopspodcast.clear-measure.com/mark-miller-on-developer-productivity"
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

            Assert.Equal("http://azuredevopspodcast.clear-measure.com/mark-miller-on-developer-productivity", result.EpisodeUrl);
        }
    }
}
