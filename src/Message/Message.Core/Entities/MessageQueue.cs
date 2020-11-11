using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Entities
{
    public class MessageQueue
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public Queue<string> MessageLines { get; set; }

        public MessageQueue()
        {
        }

        public MessageQueue(string senderUsername, string receiverUsername, Queue<string> messageLine)
        {
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            MessageLines = messageLine;
        }
    }
}
