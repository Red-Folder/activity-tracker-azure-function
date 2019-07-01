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

            var category = Categories.Where(x => x.Name == podCast.Category).FirstOrDefault();
            if (category == null)
            {
                category = new PodCastCategory(podCast.Category);
                Categories.Add(category);
            }

            category.AddPodCast(podCast);
        }
    }
}
