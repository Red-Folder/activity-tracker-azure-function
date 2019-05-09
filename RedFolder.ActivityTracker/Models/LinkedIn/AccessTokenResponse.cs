using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.LinkedIn
{
    public class AccessTokenResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonIgnore]
        public int SecondsTillExpires { get; set; }
    }
}
