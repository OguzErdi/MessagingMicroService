using Message.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Core.Repositories
{
    public interface IMessageHistoryRepository
    {
        Task<bool> UpdateMessageHistory(MessageQueue messageHistory);
        Task<MessageQueue> GetMessageHistory(string senderUsername, string receiverUsername);
    }
}
