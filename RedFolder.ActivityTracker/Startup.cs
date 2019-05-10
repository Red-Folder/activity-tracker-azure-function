using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(RedFolder.ActivityTracker.Startup))]

namespace RedFolder.ActivityTracker
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ITest, Test>();
        }


        public interface ITest
        {
            string Hello();
        }

        public class Test : ITest
        {
            public string Hello()
            {
                return "Hello World";
            }
        }

    }
}
