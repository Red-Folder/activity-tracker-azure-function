using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class FreelancerShowUnitTests
    {
        private readonly FreelancerShow _sut;

        private readonly PodCastTableEntity _sample;

        public FreelancerShowUnitTests()
        {
            _sut = new FreelancerShow();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "The Freelancers' Show",
                EpisodeName = "TFS 331: Using a CRM",
                EpisodeUrl = "http://www.podtrac.com/pts/redirect.mp3/media.devchat.tv/freelancers/TFS_331_Using_a_CRM.mp3",
                EpisodePostUrl = " https://devchat.tv/freelancers/tfs-331-using-a-crm/"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Other", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal(" https://devchat.tv/freelancers/tfs-331-using-a-crm/", result.EpisodeUrl);
        }
    }
}
