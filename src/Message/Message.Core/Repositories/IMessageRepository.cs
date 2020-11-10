using Message.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Repositories
{
    public interface IMessageRepository
    {
        List<MessageEntity> GetMessages(string senderUsername, string recieverUsername);
        MessageEntity GetLastMessage(string senderUsername, string recieverUsername);
        bool AddMessage(string senderUsername, string recieverUsername, string content);
    }
}
