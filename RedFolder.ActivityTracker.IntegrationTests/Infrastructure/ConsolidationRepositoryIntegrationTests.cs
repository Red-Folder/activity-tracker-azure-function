using Microsoft.Azure.Cosmos.Table;
using RedFolder.ActivityTracker.BeyondPod.Infrastructure;
using RedFolder.ActivityTracker.IntegrationTests.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RedFolder.ActivityTracker.IntegrationTests.Infrastructure
{
    public class ConsolidationRepositoryIntegrationTests
    {
        private readonly ConsolidationRepository _sut;

        private readonly string _tableName;
        private readonly CloudTable _cloudTable;

        public ConsolidationRepositoryIntegrationTests()
        {
            _tableName = "test" + Guid.NewGuid().ToString().Replace("-", "");
            var cloudStorageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true;");
            var tableClient = cloudStorageAccount.CreateCloudTableClient();
            _cloudTable = tableClient.GetTableReference(_tableName);
            _cloudTable.CreateIfNotExistsAsync().Wait();

            _sut = new ConsolidationRepository(_cloudTable);
        }
        ~ConsolidationRepositoryIntegrationTests()
        {
            _cloudTable.DeleteIfExistsAsync().Wait();
        }

        [Fact]
        public async Task WillSaveNewRecord()
        {
            await _sut.Save(CloudTableHelper.TableEntityPodCast);

            var list = await CloudTableHelper.GetAllCloudTableEntities(_cloudTable);

            Assert.Single(list);
            Assert.Equal(CloudTableHelper.BeyondPodPodCast.EpisodeName, list.First().EpisodeName);
        }

        [Fact]
        public async Task WillUpdateExistingRecord()
        {
            await CloudTableHelper.AddToCloudTableEntities(_cloudTable, CloudTableHelper.TableEntityPodCast);

            var existingRecord = await _sut.Get(CloudTableHelper.TableEntityPodCast.PartitionKey,
                                                CloudTableHelper.TableEntityPodCast.RowKey);
            existingRecord.EpisodePosition = 1000;

            await _sut.Save(existingRecord);

            var list = await CloudTableHelper.GetAllCloudTableEntities(_cloudTable);

            Assert.Single(list);
            Assert.Equal(1000, list.First().EpisodePosition);
        }

        [Fact]
        public async Task WillThrowExceptionIfRecordHasChanged()
        {
            await CloudTableHelper.AddToCloudTableEntities(_cloudTable, CloudTableHelper.TableEntityPodCast);

            var v1 = await _sut.Get(CloudTableHelper.TableEntityPodCast.PartitionKey,
                                                CloudTableHelper.TableEntityPodCast.RowKey);
            var v2 = await _sut.Get(CloudTableHelper.TableEntityPodCast.PartitionKey,
                                                CloudTableHelper.TableEntityPodCast.RowKey);
            v1.EpisodePosition = 1000;
            v2.EpisodePosition = 1001;

            await _sut.Save(v2);

            await Assert.ThrowsAsync<StorageException>(() => _sut.Save(v1));
        }
    }
}
