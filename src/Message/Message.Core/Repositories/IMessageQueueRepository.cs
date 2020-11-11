using Message.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Core.Repositories
{
    public interface IMessageQueueRepository
    {
        Task<bool> UpdateMessageQueue(MessageQueue messageQueue);
        Task<MessageQueue> GetMessageQueue(string senderUsername, string receiverUsername);
    }
}
