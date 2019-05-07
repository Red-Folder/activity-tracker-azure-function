﻿using Microsoft.Bot.Connector.DirectLine;
using Red_Folder.ActivityTracker.Models.ActivityBot;
using System.Threading.Tasks;

namespace Red_Folder.ActivityTracker.Services
{
    public class ActivityBotProxy
    {
        //private readonly string _directlineUrl;
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
        }
    }
}
