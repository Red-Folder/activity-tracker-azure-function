﻿using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using RedFolder.ActivityTracker.Models.BeyondPod;

namespace RedFolder.ActivityTracker.BeyondPod.Infrastructure
{
    public class ConsolidationRepository : IConsolidationRepository
    {
        private readonly CloudTable _table;

        public ConsolidationRepository(CloudTable table)
        {
            _table = table;
        }

        public async Task<PodCastTableEntity> Get(string feedName, string episodeName)
        {
            var partitionKey = ToAzureKeyString(feedName);
            var rowKey = ToAzureKeyString(episodeName);
            var retrieveOperation = TableOperation.Retrieve<Models.BeyondPod.PodCastTableEntity>(partitionKey, rowKey);

            var query = await _table.ExecuteAsync(retrieveOperation);

            return (PodCastTableEntity)query.Result;
        }

        public async Task Save(PodCastTableEntity entity)
        {
            // Ensure approriate format
            entity.PartitionKey = ToAzureKeyString(entity.PartitionKey);
            entity.RowKey = ToAzureKeyString(entity.RowKey);

            var insertOperation = TableOperation.InsertOrReplace(entity);
            await _table.ExecuteAsync(insertOperation);
        }

        private static string ToAzureKeyString(string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str
                .Where(c => c != '/'
                            && c != '\\'
                            && c != '#'
                            && c != '/'
                            && c != '?'
                            && !char.IsControl(c)))
                sb.Append(c);
            return sb.ToString();
        }
    }
}