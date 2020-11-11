using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Entities
{
    public class MessageEntity
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string Content { get; set; }

        public MessageEntity()
        {
        }

        public MessageEntity(string senderUsername, string receiverUsername, string content)
        {
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            Content = content;
        }
    }
}
