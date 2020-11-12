using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Message.Core.Entities
{
    public class MessageHistroy
    {
        public string MessageHistoryKey { get; set; }
        public List<string> Participants { get; set; }
        public List<MessageEntity> MessageEntities { get; set; }

        public MessageHistroy(string messageHistoryKey, List<string> participants, List<MessageEntity> messageEntities)
        {
            MessageHistoryKey = messageHistoryKey;
            Participants = participants;
            MessageEntities = messageEntities;
        }
    }
}
