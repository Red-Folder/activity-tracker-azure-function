using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using RedFolder.ActivityTracker.Models.BeyondPod;
using System;
using Xunit;

namespace RedFolder.ActivityTracker.BeyondPod.UnitTests.Converters.Handlers
{
    public class BaseHandlerUnitTests
    {
        private readonly BaseHandler _sut;

        public BaseHandlerUnitTests()
        {
            _sut = new BaseHandler();
        }

        [Fact]
        public void ConvertsAllFields()
        {
            var source = new PodCastTableEntity
            {
                Created  = DateTime.Now.AddDays(-1),
                Playing = true,
                FeedName = "Test Feed Name",
                FeedUrl = "Test Feed Url",
                EpisodeName = "Test Episode Name",
                EpisodeUrl = "Test Episode Url",
                EpisodeFile = "Test Episode File",
                EpisodePostUrl = "Test Post Episode Url",
                EpisodeMime = "Test Episode Mime",
                EpisodeSummary = "Test Episode Summary",
                EpisodeDuration = 123,
                EpisodePosition = 101,
                Artist = "Test Artist",
                Album = "Test Albulm",
                Track = "Test Track",
                Actioned = true
            };

            var result = _sut.Convert(source);

            Assert.Equal(source.Created, result.Created);
            Assert.Equal(source.Playing, result.Playing);
            Assert.Equal(source.FeedName, result.FeedName);
            Assert.Equal(source.FeedUrl, result.FeedUrl);
            Assert.Equal(source.EpisodeName, result.EpisodeName);
            Assert.Equal(source.EpisodeUrl, result.EpisodeUrl);
            Assert.Equal(source.EpisodeFile, result.EpisodeFile);
            Assert.Equal(source.EpisodePostUrl, result.EpisodePostUrl);
            Assert.Equal(source.EpisodeMime, result.EpisodeMime);
            Assert.Equal(source.EpisodeSummary, result.EpisodeSummary);
            Assert.Equal(source.EpisodeDuration, result.EpisodeDuration);
            Assert.Equal(source.EpisodePosition, result.EpisodePosition);
            Assert.Equal(source.Artist, result.Artist);
            Assert.Equal(source.Album, result.Album);
            Assert.Equal(source.Track, result.Track);
            Assert.Equal(source.Actioned, result.Actioned);
            Assert.Equal("", result.Category);
        }
    }
}
