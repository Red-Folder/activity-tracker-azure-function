namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public interface IHandler
    {
        void AddInner(IHandler inner);
        Models.PodCast Convert(Models.BeyondPod.PodCastTableEntity source);
    }
}
