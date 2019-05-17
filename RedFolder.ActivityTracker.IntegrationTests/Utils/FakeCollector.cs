using Microsoft.Azure.WebJobs;
using System.Collections.Generic;

namespace RedFolder.ActivityTracker.IntegrationTests.Utils
{
    public class FakeCollector<T> : ICollector<T>
    {
        public List<T> UnderlyingList { get; } = new List<T>();

        public void Add(T item)
        {
            UnderlyingList.Add(item);
        }
    }
}
