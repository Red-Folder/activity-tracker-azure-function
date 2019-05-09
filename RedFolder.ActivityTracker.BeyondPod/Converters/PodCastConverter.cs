using RedFolder.ActivityTracker.BeyondPod.Converters.Handlers;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.ActivityTracker.BeyondPod.Converters
{
    public class PodCastConverter
    {
        private readonly IHandler _rootHandler;

        public PodCastConverter()
        {
            var handlers = new List<IHandler>
            {
                new CategoryHandler("Security","SANS Internet Storm Center Daily Network Cyber Security and Information Security Podcast"),
                new CategoryHandler("Security","Risky Business"),
                new CategoryHandler("Security","Troy Hunt's Weekly Update Podcast"),

                new CategoryHandler("General Development","The InfoQ Podcast"),
                new CategoryHandler("General Development","Weekly Dev Tips"),
                new CategoryHandler("General Development","Hanselminutes"),
                new CategoryHandler("General Development","Software Engineering Radio - The Podcast for Professional Software Developers"),
                new CategoryHandler("General Development","Legacy Code Rocks"),

                new CategoryHandler("JavaScript","JavaScript Jabber"),

                new CategoryHandler("Angular","Adventures in Angular"),

                new CategoryHandler(".Net & C#","NET Rocks"),

                new CategoryHandler("React & Redux","React Round Up"),
                new CategoryHandler("React & Redux","The React Podcast"),

                new CategoryHandler("Containers","PodCTL - Containers | Kubernetes | OpenShift"),

                new CategoryHandler("Azure & AWS","The Azure Podcast"),
                new CategoryHandler("Azure & AWS","AWS Podcast"),
                new CategoryHandler("Azure & AWS","DevOps on AWS Radio"),
                new CategoryHandler("Azure & AWS","AWS TechChat"),

                new CategoryHandler("Fun","More or Less: Behind the Stats"),
                new CategoryHandler("Fun","Friday Night Comedy from BBC Radio 4"),
                new CategoryHandler("Fun","In Our Time: Science"),

                new CategoryHandler("Leadership","Engineering Culture by InfoQ"),

                new CategoryHandler("Other","RunAs Radio"),
                new CategoryHandler("Other","The Freelancers'Show"),

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
