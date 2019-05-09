using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.LinkedIn.Raw
{
    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
