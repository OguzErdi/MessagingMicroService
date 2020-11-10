using System;
using System.Collections.Generic;
using System.Text;
using Message.Core.Entities;

namespace Message.Application.Interfaces
{
    public interface IMessageService
    {
        bool AddMessage(string senderUsername, string recieverUsername, string content);

        MessageEntity GetMessage(string senderUsername, string recieverUsername);
    }
}
