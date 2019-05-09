using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.LinkedIn.Raw
{
    public class ShareRequest
    {
        public ShareRequest()
        {
            Text = new ShareText { Text = "Test Share" };
            Distribution = new ShareDistributionTargets { LinkedInDistributionTarget = new LinkedInDistributionTarget() }; 
        }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("text")]
        public ShareText Text { get; set; }
 
        [JsonProperty("distribution")]
        public ShareDistributionTargets Distribution { get; set; }

    }
}
