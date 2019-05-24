using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class PodCtlUnitTests
    {
        private readonly PodCtl _sut;

        private readonly PodCastTableEntity _sample;

        public PodCtlUnitTests()
        {
            _sut = new PodCtl();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "PodCTL - Containers | Kubernetes | OpenShift",
                EpisodeName = "Operators and OperatorHub",
                EpisodeUrl = "https://www.buzzsprout.com/110399/1077212-operators-and-operatorhub.mp3",
                EpisodePostUrl = "https://www.buzzsprout.com/110399/1077212"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Containers", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://www.buzzsprout.com/110399/1077212", result.EpisodeUrl);
        }
    }
}
