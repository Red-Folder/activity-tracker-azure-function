using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class AdventuresInAngularUnitTests
    {
        private readonly AdventuresInAngular _sut;

        private readonly PodCastTableEntity _sample;

        public AdventuresInAngularUnitTests()
        {
            _sut = new AdventuresInAngular();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Adventures in Angular Only",
                EpisodeName = "AiA 240: RxJS and Observable Forms in Angular with Sander Elias",
                EpisodeUrl = "http://www.podtrac.com/pts/redirect.mp3/devchat.cachefly.net/angular/AiA_240_RxJS_and_Observable_Forms_in_Angular_with_Sander_Elias.mp3",
                EpisodePostUrl = "https://devchat.tv/adv-in-angular/aia-240-rxjs-and-observable-forms-in-angular-with-sander-elias"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Angular", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://devchat.tv/adv-in-angular/aia-240-rxjs-and-observable-forms-in-angular-with-sander-elias", result.EpisodeUrl);
        }

        [Fact]
        public void CleansName()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Adventures in Angular", result.FeedName);
        }
    }
}
