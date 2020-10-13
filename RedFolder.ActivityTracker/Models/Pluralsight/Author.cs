using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.Pluralsight
{
    public class Author
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("handle")]
        public string Handle { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
