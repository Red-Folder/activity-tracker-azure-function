using Microsoft.Bot.Connector.DirectLine;
using RedFolder.ActivityTracker.Models.ActivityBot;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.Services
{
    public class ActivityBotProxy
    {
        private readonly string _secret;

        public ActivityBotProxy(string secret)
        {
            _secret = secret;
        }

        public async Task Broadcast(Payload payload)
        {
            var client = new DirectLineClient(_secret);
            var conversation = await client.Conversations.StartConversationAsync();

            await client.Conversations.PostActivityAsync(conversation.ConversationId, payload.ToActivityBotActivity());

            await client.Conversations.PostActivityAsync(conversation.ConversationId, new Activity
            {
                Type = ActivityTypes.EndOfConversation,
                From = new ChannelAccount
                {
                    Id = "ActivityTracker",
                    Name = "Activity Tracker functions"
                }
            });
        }
    }
}
