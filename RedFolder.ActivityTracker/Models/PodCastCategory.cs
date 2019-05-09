using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedFolder.ActivityTracker.Models
{
    public class PodCastCategory
    {
        public PodCastCategory()
        {

        }

        public PodCastCategory(String name)
        {
            Name = name;
        }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("podCasts")]
        public List<PodCast> PodCasts { get; set; }

        [JsonIgnore]
        public long TotalDuration
        {
            get
            {
                if (PodCasts == null || PodCasts.Count() == 0) return 0;
                return PodCasts.Aggregate((long)0, (acc, x) => acc + x.EpisodeDuration);
            }
        }

        public void AddPodCast(PodCast podCast)
        {
            if (PodCasts == null) PodCasts = new List<PodCast>();
            PodCasts.Add(podCast);
        }

    }
}
