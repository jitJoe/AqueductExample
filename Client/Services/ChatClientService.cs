using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aqueduct.Client;
using AqueductExample.Shared;

namespace AqueductExample.Client.Services
{
    public class ChatClientService : ClientService, IChatClientService, IChatClientLocalService
    {
        private readonly Dictionary<Guid, Action<ChatMessage>> _callbacks = new Dictionary<Guid, Action<ChatMessage>>();
        
        public Task SendMessageAsync(ChatMessage message)
        {
            foreach (var callback in _callbacks.Values)
            {
                callback(message);
            }

            return Task.CompletedTask;
        }

        public Guid SubscribeToMessages(Action<ChatMessage> onMessage)
        {
            var subscriptionId = Guid.NewGuid();

            _callbacks.Add(subscriptionId, onMessage);
            
            return subscriptionId;
        }
    }
}