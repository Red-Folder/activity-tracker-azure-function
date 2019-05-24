using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class TwoThousandBooksUnitTestsUnitTests
    {
        private readonly TwoThousandBooks _sut;

        private readonly PodCastTableEntity _sample;

        public TwoThousandBooksUnitTestsUnitTests()
        {
            _sut = new TwoThousandBooks();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "2000 Books for Ambitious Entrepreneurs - Author Interviews and Book Summaries",
                EpisodeName = "197[Self Help] As a Man Thinketh - James Allen | The 1 Key to never be a victim of circumstances",
                EpisodeUrl = "https://traffic.libsyn.com/secure/2000books/As_a_Man_thinketh_-_Purpose.mp3?dest-id=380666",
                EpisodePostUrl = "https://2000books.libsyn.com/197self-help-as-a-man-thinketh-james-allen-the-1-key-to-never-be-a-victim-of-circumstances"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Leadership", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://2000books.libsyn.com/197self-help-as-a-man-thinketh-james-allen-the-1-key-to-never-be-a-victim-of-circumstances", result.EpisodeUrl);
        }
    }
}
