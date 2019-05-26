using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using RedFolder.ActivityTracker.BeyondPod;
using RedFolder.ActivityTracker.BeyondPod.Converters;
using RedFolder.ActivityTracker.IntegrationTests.Utils;
using RedFolder.ActivityTracker.Models.BeyondPod;
using RedFolder.ActivityTracker.NewPodCasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RedFolder.ActivityTracker.IntegrationTests.NewPodCasts
{
    public class BeyondPodRawDataConsolidationIntegrationTests
    {
        private readonly ConsolidationHandler _handler;
        private readonly BeyondPodRawDataConsolidation _sut;

        private readonly string _tableName;
        private readonly CloudTable _cloudTable;

        private readonly FakeCollector<Models.PodCast> _collector;

        private readonly Mock<ILogger> _logger;

        public BeyondPodRawDataConsolidationIntegrationTests()
        {
            var _handler = new ConsolidationHandler(new PodCastConverter());
            _sut = new BeyondPodRawDataConsolidation(_handler);

            _tableName = "test" + Guid.NewGuid().ToString().Replace("-", "");
            var cloudStorageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true;");
            var tableClient = cloudStorageAccount.CreateCloudTableClient();
            _cloudTable = tableClient.GetTableReference(_tableName);
            _cloudTable.CreateIfNotExistsAsync().Wait();

            _collector = new FakeCollector<Models.PodCast>();
            _logger = new Mock<ILogger>();

        }

        ~BeyondPodRawDataConsolidationIntegrationTests()
        {
            _cloudTable.DeleteIfExistsAsync().Wait();
        }

        [Fact]
        public async Task CreatesAnEntryInTheTable_IfNew()
        {
            await _sut.RunAsync(StandardPodCast, _cloudTable, _collector, _logger.Object);

            var list = await GetAllCloudTableEntities();

            Assert.Single(list);
            Assert.Equal(StandardPodCast.EpisodeName, list.First().EpisodeName);
        }

        [Fact]
        public async Task UpdatesAnEntryInTheTable_IfAlreadyExists()
        {
            await AddToCloudTableEntities(new PodCastTableEntity
            {
                PartitionKey = StandardPodCast.FeedName,
                RowKey = StandardPodCast.EpisodeName,
                Created = DateTime.Now,
                EpisodeDuration = StandardPodCast.EpisodeDuration,
                EpisodePosition = StandardPodCast.EpisodePosition
            });

            var newData = StandardPodCast;
            newData.EpisodePosition = 1000;

            await _sut.RunAsync(newData, _cloudTable, _collector, _logger.Object);

            var list = await GetAllCloudTableEntities();

            Assert.Single(list);
            Assert.Equal(1000, list.First().EpisodePosition);
        }

        private async Task<List<PodCastTableEntity>> GetAllCloudTableEntities()
        {
            var get = await _cloudTable.ExecuteQuerySegmentedAsync(new TableQuery<PodCastTableEntity>(), new TableContinuationToken());

            return get.Results;
        }

        private async Task AddToCloudTableEntities(PodCastTableEntity entity)
        {
            await _cloudTable.ExecuteAsync(TableOperation.Insert(entity));
        }

        private Models.BeyondPod.PodCast StandardPodCast = new Models.BeyondPod.PodCast
        {
            Created = DateTime.Now,
            Playing = true,
            FeedName = "NET Rocks",
            FeedUrl = "http://feeds.feedburner.com/netRocksFullMp3Downloads",
            EpisodeName = ".NET Core 3 and Beyond with Scott Hunter",
            EpisodeUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~5/csNN236Wlys/e36199af23789d048885158bc55257d7.mp3",
            EpisodeFile = "/storage/emulated/0/Android/data/mobi.beyondpod/files/BeyondPod/Podcasts/NET Rocks/NET Rocks-2031992760.mp3",
            EpisodePostUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~3/A90Z67z1414/default.aspx",
            EpisodeMime = "audio/mpeg",
            EpisodeSummary = "Build is over - what did we learn? Carl and Richard talk to Scott Hunter about the various announcements at Build connection with .NET - including the delivery date of .NET Core 3 and what happens beyond! The conversation digs into switching to a routine delivery model for .NET, so that you can anticipate when you'll need to implement the new version of the framework. Scott also talks about new features coming in C# 8, including the fact that C# 8 is only for .NET Core 3 and above... things are",
            EpisodeDuration = 3042,
            EpisodePosition = 100,
            Artist = "NET Rocks",
            Album = "NET Rocks",
            Track = ".NET Core 3 and Beyond with Scott Hunter"
        };
    }
}
