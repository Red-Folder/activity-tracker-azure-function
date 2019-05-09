using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.LinkedIn.Raw
{
    public class LinkedInDistributionTarget
    {
        [JsonProperty("visibleToGuest")]
        public bool VisibileToGuest => true;
    }
}
