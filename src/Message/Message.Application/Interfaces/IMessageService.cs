using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Message.Core.Entities;
using Message.Core.Results;

namespace Message.Application.Interfaces
{
    public interface IMessageService
    {
        Task<IResult> AddMessage(string messageLine, string senderUsername, string receiverUsername);
        Task<IDataResult<MessageHistroy>> GetMessageHistory(string who, string toWhom);
        Task<IDataResult<string>> GetLastMessage(string senderUsername, string receiverUsername);
    }
}
