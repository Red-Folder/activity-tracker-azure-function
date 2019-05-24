using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class SixFigureDeveloperUnitTests
    {
        private readonly SixFigureDeveloper _sut;

        private readonly PodCastTableEntity _sample;

        public SixFigureDeveloperUnitTests()
        {
            _sut = new SixFigureDeveloper();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "The 6 Figure Developer Podcast",
                EpisodeName = "Episode 092 – AI on the Edge and IoT with Jared Rhodes",
                EpisodeUrl = "http://media.blubrry.com/6figuredev/content.blubrry.com/6figuredev/6_Figure_Developer-092-Jared_Rhodes.mp3",
                EpisodePostUrl = "https://6figuredev.com/podcast/episode-092-ai-on-the-edge-and-iot-with-jared-rhodes/"
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

            Assert.Equal("https://6figuredev.com/podcast/episode-092-ai-on-the-edge-and-iot-with-jared-rhodes/", result.EpisodeUrl);
        }
    }
}
