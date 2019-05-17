using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(RedFolder.ActivityTracker.Startup))]

namespace RedFolder.ActivityTracker
{
    
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<ITest, Test>();

            BeyondPod.DependanceInjection.RegisterServices(builder.Services);
        }
    }
}
