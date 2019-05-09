using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.ActivityTracker.Models
{
    public class PodCastActivity
    {
        [JsonProperty("categories")]
        public List<PodCastCategory> Categories;

        [JsonIgnore]
        public long TotalDuration
        {
            get
            {
                if (Categories == null || Categories.Count() == 0) return 0;

                return Categories.Aggregate((long)0, (acc, x) => acc + x.TotalDuration);
            }
        }

        public void Add(PodCast podCast)
        {
            if (Categories == null) Categories = new List<PodCastCategory>();

            var categoryName = PodCastCategory(podCast);
            var category = Categories.Where(x => x.Name == categoryName).FirstOrDefault();
            if (category == null)
            {
                category = new PodCastCategory(categoryName);
                Categories.Add(category);
            }

            category.AddPodCast(podCast);
        }

        private String PodCastCategory(PodCast podCast)
        {
            switch (podCast.FeedName)
            {
                case "SANS Internet Storm Center Daily Network Cyber Security and Information Security Podcast":
                case "Risky Business":
                case "Troy Hunt's Weekly Update Podcast":
                    return "Security";

                case "The InfoQ Podcast":
                case "Weekly Dev Tips":
                case "Hanselminutes":
                case "Software Engineering Radio - The Podcast for Professional Software Developers":
                case "Legacy Code Rocks":
                    return "General Development";

                case "JavaScript Jabber":
                    return "JavaScript";

                case "Adventures in Angular":
                    return "Angular";

                case "NET Rocks":
                    return ".Net & C#";

                case "React Round Up":
                case "The React Podcast":
                    return "React & Redux";

                case "PodCTL - Containers | Kubernetes | OpenShift":
                    return "Containers";

                case "The Azure Podcast":
                case "AWS Podcast":
                case "DevOps on AWS Radio":
                case "AWS TechChat":
                    return "Azure & AWS";

                case "More or Less: Behind the Stats":
                case "Friday Night Comedy from BBC Radio 4":
                case "In Our Time: Science":
                    return "Fun";

                case "Engineering Culture by InfoQ":
                    return "Leadership";

                case "RunAs Radio":
                case "The Freelancers'Show":
                default:
                    return "Other";
            }
        }

    }
}
