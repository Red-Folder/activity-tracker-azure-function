using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.ActivityTracker.BeyondPod.Converters
{
    public class PodCastConverter : IPodCastConverter
    {
        private readonly IHandler _rootHandler;

        public PodCastConverter()
        {
            var handlers = new List<IHandler>
            {
                new SansInternetStormCenterDaily(),
                new RiskyBusiness(),
                new TroyHunt(),
                new DarknetDiaries(),

                new InfoQ(),
                new WeeklyDevTips(),
                new HanselMinutes(),
                new SoftwareEngineeringRadio(),
                new LegacyCodeRocks(),
                new SixFigureDeveloper(),
                new HerdingCode(),

                new JavaScriptJabber(),

                new AdventuresInAngular(),

                new DotNetRocks(),

                new ReactRoundup(),
                new ReactPodcast(),

                new PodCtl(),

                new AzurePodcast(),
                new AzureDevOps(),
                new AwsPodcast(),
                new AwsDevOps(),
                new AwsTechChat(),

                new MoreOrLess(),
                new FridayNightComedy(),
                new InOurTime(),
                new InfiniteMonkeyCage(),

                new InfoQCulture(),
                new TwoThousandBooks(),
                new BetterROI(),
                new FreelancerShow(),
                new BrainScience(),

                new RunAsRadio(),
                new AdventuresInDevOps(),
                new Kubernetes(),

                new DefaultCategoryHandler(),

                new BaseHandler()
            };

            _rootHandler = handlers.First();

            IHandler parent = null;
            foreach (var handler in handlers)
            {
                if (parent != null)
                {
                    parent.AddInner(handler);
                }

                parent = handler;
            }
        }

        public Models.PodCast Convert(Models.BeyondPod.PodCastTableEntity source)
        {
            return _rootHandler.Convert(source);
        }
    }
}
