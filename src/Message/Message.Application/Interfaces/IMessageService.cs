using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Message.Core.Entities;

namespace Message.Application.Interfaces
{
    public interface IMessageService
    {
        Task<bool> AddMessage(string messageLine, string senderUsername, string receiverUsername);
        Task<string> GetLastMessage(string senderUsername, string receiverUsername);
        Task<MessageHistroy> GetMessageHistory(string who, string toWhom);
    }
}
