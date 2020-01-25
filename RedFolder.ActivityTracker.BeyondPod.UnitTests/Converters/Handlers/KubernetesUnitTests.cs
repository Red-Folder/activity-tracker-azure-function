using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class KubernetesUnitTests
    {
        private readonly Kubernetes _sut;

        private readonly PodCastTableEntity _sample;

        public KubernetesUnitTests()
        {
            _sut = new Kubernetes();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Kubernetes Podcast from Google",
                EpisodeName = "OpenShift and Kubernetes, with Clayton Coleman",
                EpisodeUrl = "https://kubernetespodcast.com/episodes/KPfGep085.mp3",
                EpisodePostUrl = "https://kubernetespodcast.com/episode/085-openshift-and-kubernetes/"
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

            Assert.Equal("https://kubernetespodcast.com/episode/085-openshift-and-kubernetes/", result.EpisodeUrl);
        }
    }
}
