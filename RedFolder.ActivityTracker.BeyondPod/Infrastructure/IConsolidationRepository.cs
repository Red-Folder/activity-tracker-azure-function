using RedFolder.ActivityTracker.Models.BeyondPod;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.BeyondPod.Infrastructure
{
    public interface IConsolidationRepository
    {
        Task<PodCastTableEntity> Get(string feedName, string episodeName);
        Task Save(PodCastTableEntity entity);
    }
}
