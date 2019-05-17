using Microsoft.Azure.WebJobs;
using RedFolder.ActivityTracker.Models;

namespace RedFolder.ActivityTracker.BeyondPod.Infrastructure
{
    public class NewPodCastQueue : INewPodCastQueue
    {
        private readonly ICollector<PodCast> _queue;

        public NewPodCastQueue(ICollector<PodCast> queue)
        {
            _queue = queue;
        }

        public void Add(PodCast podCast)
        {
            _queue.Add(podCast);
        }
    }
}
