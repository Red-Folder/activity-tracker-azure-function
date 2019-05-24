using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class SoftwareEngineeringRadioUnitTests
    {
        private readonly SoftwareEngineeringRadio _sut;

        private readonly PodCastTableEntity _sample;

        public SoftwareEngineeringRadioUnitTests()
        {
            _sut = new SoftwareEngineeringRadio();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Software Engineering Radio - The Podcast for Professional Software Developers",
                EpisodeName = "366: Test Automation",
                EpisodeUrl = "http://feedproxy.google.com/~r/se-radio/~5/s1Z7O3Vlm4M/366-Test-Automation.mp3",
                EpisodePostUrl = "http://feedproxy.google.com/~r/se-radio/~3/EJth7FBvjnc/"
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

            Assert.Equal("http://feedproxy.google.com/~r/se-radio/~3/EJth7FBvjnc/", result.EpisodeUrl);
        }
    }
}
