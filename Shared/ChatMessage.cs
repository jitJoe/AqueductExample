using System;

namespace AqueductExample.Shared
{
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SentAt { get; set; }
    }
}