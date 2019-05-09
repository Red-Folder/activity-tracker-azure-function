using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.ActivityBot
{
    public class Payload
    {
        public PayloadType Type { get; set; }
        public object Contents { get; set; }

        public Activity ToActivityBotActivity()
        {
            var json = JsonConvert.SerializeObject(Contents);

            return new Activity
            {
                Type = ActivityTypes.Message,
                From = new ChannelAccount
                {
                    Id = "ActivityTracker",
                    Name = "Activity Tracker functions"
                },
                Text = $"broadcast {Type} {json}"
            };
        }
    }
}
