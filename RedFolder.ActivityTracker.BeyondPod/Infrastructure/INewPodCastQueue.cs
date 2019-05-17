namespace RedFolder.ActivityTracker.BeyondPod.Infrastructure
{
    public interface INewPodCastQueue
    {
        void Add(Models.PodCast podCast);
    }
}
