namespace RedFolder.ActivityTracker.Services.PodCast
{
    public interface IHandler
    {
        void AddInner(IHandler inner);
        Models.PodCast Convert(Models.PodCastTableEntity source);
    }
}
