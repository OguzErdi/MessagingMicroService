﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Entities
{
    public class MessageEntity
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string MessageLine { get; set; }

        public MessageEntity(string senderUsername, string receiverUsername, string messageLine)
        {
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            MessageLine = messageLine;
        }
    }
}
