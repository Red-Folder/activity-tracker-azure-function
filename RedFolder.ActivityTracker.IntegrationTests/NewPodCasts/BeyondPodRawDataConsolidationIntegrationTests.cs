using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using Moq;
using RedFolder.ActivityTracker.BeyondPod;
using RedFolder.ActivityTracker.BeyondPod.Converters;
using RedFolder.ActivityTracker.IntegrationTests.Utils;
using RedFolder.ActivityTracker.Models.BeyondPod;
using RedFolder.ActivityTracker.NewPodCasts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RedFolder.ActivityTracker.IntegrationTests.NewPodCasts
{
    public class BeyondPodRawDataConsolidationIntegrationTests
    {
        private readonly BeyondPodRawDataConsolidation _sut;

        private readonly string _tableName;
        private readonly CloudTable _cloudTable;

        private readonly FakeCollector<Models.PodCast> _collector;

        private readonly Mock<ILogger> _logger;

        public BeyondPodRawDataConsolidationIntegrationTests()
        {
            var handler = new ConsolidationHandler(new PodCastConverter());
            _sut = new BeyondPodRawDataConsolidation(handler);

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
            await _sut.RunAsync(CloudTableHelper.BeyondPodPodCast, _cloudTable, _collector, _logger.Object);

            var list = await CloudTableHelper.GetAllCloudTableEntities(_cloudTable);

            Assert.Single(list);
            Assert.Equal(CloudTableHelper.BeyondPodPodCast.EpisodeName, list.First().EpisodeName);
        }

        [Fact]
        public async Task UpdatesAnEntryInTheTable_IfAlreadyExists()
        {
            await CloudTableHelper.AddToCloudTableEntities(_cloudTable, new PodCastTableEntity
            {
                PartitionKey = CloudTableHelper.BeyondPodPodCast.FeedName,
                RowKey = CloudTableHelper.BeyondPodPodCast.EpisodeName,
                Created = DateTime.Now,
                EpisodeDuration = CloudTableHelper.BeyondPodPodCast.EpisodeDuration,
                EpisodePosition = CloudTableHelper.BeyondPodPodCast.EpisodePosition
            });

            var newData = CloudTableHelper.BeyondPodPodCast;
            newData.EpisodePosition = 1000;

            await _sut.RunAsync(newData, _cloudTable, _collector, _logger.Object);

            var list = await CloudTableHelper.GetAllCloudTableEntities(_cloudTable);

            Assert.Single(list);
            Assert.Equal(1000, list.First().EpisodePosition);
        }
    }
}
