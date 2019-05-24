using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class JavaScriptJabberUnitTests
    {
        private readonly JavaScriptJabber _sut;

        private readonly PodCastTableEntity _sample;

        public JavaScriptJabberUnitTests()
        {
            _sut = new JavaScriptJabber();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "JavaScript Jabber Only",
                EpisodeName = "JSJ 365: Do You Need a Front-End Framework?",
                EpisodeUrl = "https://media.devchat.tv/js-jabber/JSJ_365_Do_You_Need_a_Front_End_Framework.mp3",
                EpisodePostUrl = "https://devchat.tv/js-jabber/jsj-365-do-you-need-a-front-end-framework/"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("JavaScript", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://devchat.tv/js-jabber/jsj-365-do-you-need-a-front-end-framework/", result.EpisodeUrl);
        }

        [Fact]
        public void CleansName()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("JavaScript Jabber", result.FeedName);
        }
    }
}
