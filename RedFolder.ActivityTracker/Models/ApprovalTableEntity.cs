using Microsoft.Azure.Cosmos.Table;
using System;

namespace RedFolder.ActivityTracker.Models
{
    public class ApprovalTableEntity: TableEntity
    {
        public ApprovalTableEntity() : base()
        {
        }

        public ApprovalTableEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {
        }

        public string EventName => PartitionKey;

        public string InstanceId => RowKey;

        public DateTime Expires { get; set; }

        public bool Expired => Expires < DateTime.Now;

        public string ImageUrl { get; set; }

        public int WeekNumber { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
