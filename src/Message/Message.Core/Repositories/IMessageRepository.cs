using Message.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Core.Repositories
{
    public interface IMessageRepository
    {
        Task<bool> AddMessage(MessageEntity messageEntity);
        Task<MessageEntity> GetLastMessage(string senderUsername, string recieverUsername);
        Task<List<MessageEntity>> GetMessages(string senderUsername, string recieverUsername);
    }
}
