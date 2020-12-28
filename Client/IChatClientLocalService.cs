using System;
using AqueductExample.Shared;

namespace AqueductExample.Client
{
    public interface IChatClientLocalService
    {
        Guid SubscribeToMessages(Action<ChatMessage> onMessage);
    }
}