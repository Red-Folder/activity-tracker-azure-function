using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.LinkedIn.Raw
{
    public class ShareDistributionTargets
    {
        [JsonProperty("linkedinDistributionTarget")]
        public LinkedInDistributionTarget LinkedInDistributionTarget { get; set; }
    }
}
