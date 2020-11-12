using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Entities
{
    public class MessageQueue
    {
        public string MessageQueueKey { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public Queue<string> MessageLines { get; set; }

        public MessageQueue()
        {
        }

        public MessageQueue(string messageQueueKey, string senderUsername, string receiverUsername, Queue<string> messageLines)
        {
            MessageQueueKey = messageQueueKey;
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            MessageLines = messageLines;
        }
    }
}
