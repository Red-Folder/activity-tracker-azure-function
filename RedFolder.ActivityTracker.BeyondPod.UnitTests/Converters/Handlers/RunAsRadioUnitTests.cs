using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class RunAsRadioUnitTests
    {
        private readonly RunAsRadio _sut;

        private readonly PodCastTableEntity _sample;

        public RunAsRadioUnitTests()
        {
            _sut = new RunAsRadio();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "RunAs Radio",
                EpisodeName = "Using Query Store to Help Developers Understand Query Performance with Erin Stellato",
                EpisodeUrl = "http://www.podtrac.com/pts/redirect.mp3/s3.amazonaws.com/runas/runasradio_0632_query_store.mp3",
                EpisodePostUrl = "http://feedproxy.google.com/~r/RunasRadio/~3/5gqNMw2W2zU/default.aspx"
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

            Assert.Equal("http://feedproxy.google.com/~r/RunasRadio/~3/5gqNMw2W2zU/default.aspx", result.EpisodeUrl);
        }
    }
}
