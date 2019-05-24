using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class HerdingCodeUnitTests
    {
        private readonly HerdingCode _sut;

        private readonly PodCastTableEntity _sample;

        public HerdingCodeUnitTests()
        {
            _sut = new HerdingCode();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Herding Code",
                EpisodeName = "Herding Code 233: Dino Esposito on Blazor, ASP.NET Core, Writing Technical Books, and Machine Learning",
                EpisodeUrl = "http://feedproxy.google.com/~r/HerdingCode/~5/XM6WlP3H9Wo/HerdingCode-0233-Dino-Esposito.mp3",
                EpisodePostUrl = "http://feedproxy.google.com/~r/HerdingCode/~3/Wu2w2nkdq18/"
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

            Assert.Equal("http://feedproxy.google.com/~r/HerdingCode/~3/Wu2w2nkdq18/", result.EpisodeUrl);
        }
    }
}
