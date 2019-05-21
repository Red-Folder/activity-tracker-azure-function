using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class TroyHuntUnitTests
    {
        private readonly TroyHunt _sut;

        private readonly PodCastTableEntity _sample;

        public TroyHuntUnitTests()
        {
            _sut = new TroyHunt();
            _sut.AddInner(new BaseHandler());
            _sample = new PodCastTableEntity
            {
                FeedName = "Troy Hunt's Weekly Update Podcast",
                EpisodeName = "Weekly Update 139",
                EpisodeUrl = "https://traffic.omny.fm/d/clips/1439345f-6152-486d-a9c2-a6bf0067f2b7/3ba9af7f-3bfb-48fd-aae7-a6bf00689c10/dcb87cf4-9587-4022-932c-aa520091d5ff/audio.mp3?utm_source=Podcast&in_playlist=fde26e49-9fb8-457d-8f16-a6bf00696676&t=1558293033",
                EpisodePostUrl = "https://omny.fm/shows/troy-hunt-weekly-update/weekly-update-139"
            };
        }

        [Fact]
        public void SetsCategory()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("Security", result.Category);
        }

        [Fact]
        public void SetsEpisodeUrl()
        {
            var result = _sut.Convert(_sample);

            Assert.Equal("https://omny.fm/shows/troy-hunt-weekly-update/weekly-update-139", result.EpisodeUrl);
        }
    }
}
