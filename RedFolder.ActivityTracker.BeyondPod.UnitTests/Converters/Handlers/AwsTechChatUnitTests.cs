using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class AwsTechChatUnitTests
    {
        private readonly AwsTechChat _sut;

        private readonly PodCastTableEntity _sample;

        public AwsTechChatUnitTests()
        {
            _sut = new AwsTechChat();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "AWS TechChat",
                EpisodeName = "Episode 47 - Application Security Special",
                EpisodeUrl = "http://feeds.soundcloud.com/stream/625279989-user-684142981-episode-47-security-special.mp3",
                EpisodePostUrl = "https://soundcloud.com/user-684142981/episode-47-security-special"
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

            Assert.Equal("https://soundcloud.com/user-684142981/episode-47-security-special", result.EpisodeUrl);
        }
    }
}
