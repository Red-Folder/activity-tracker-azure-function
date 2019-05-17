using Microsoft.Extensions.DependencyInjection;
using RedFolder.ActivityTracker.BeyondPod.Converters;

namespace RedFolder.ActivityTracker.BeyondPod
{
    public class DependanceInjection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPodCastConverter, PodCastConverter>();
        }
    }
}
