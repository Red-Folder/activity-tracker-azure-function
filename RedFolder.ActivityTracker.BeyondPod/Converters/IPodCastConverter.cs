using RedFolder.ActivityTracker.Models.BeyondPod;

namespace RedFolder.ActivityTracker.BeyondPod.Converters
{
    public interface IPodCastConverter
    {
        Models.PodCast Convert(PodCastTableEntity source);
    }
}