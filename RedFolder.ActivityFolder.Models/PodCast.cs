using Newtonsoft.Json;
using System;

namespace RedFolder.ActivityTracker.Models
{
    public class PodCast
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("feedName")]
        public string FeedName { get; set; }

        [JsonProperty("feedUrl")]
        public string FeedUrl { get; set; }

        [JsonProperty("episodeName")]
        public string EpisodeName { get; set; }

        [JsonProperty("episodeUrl")]
        public string EpisodeUrl { get; set; }

        [JsonProperty("episodeSummary")]
        public string EpisodeSummary { get; set; }

        [JsonProperty("episodeDuration")]
        public long EpisodeDuration { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }
}
