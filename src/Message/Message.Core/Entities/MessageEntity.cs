using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Entities
{
    public class MessageEntity
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string MessageLine { get; set; }
        public DateTime Time { get; set; }

        public MessageEntity()
        {
        }

        public MessageEntity(string senderUsername, string receiverUsername, string messageLine, DateTime time)
        {
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            MessageLine = messageLine;
            Time = time;
        }
    }
}
