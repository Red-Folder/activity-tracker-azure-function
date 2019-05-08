namespace Red_Folder.ActivityTracker.Services.PodCast
{
    public interface IHandler
    {
        void AddInner(IHandler inner);
        Models.PodCast Convert(Models.PodCastTableEntity source);
    }
}
