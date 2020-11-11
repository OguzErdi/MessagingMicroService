﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Message.Core.Entities;

namespace Message.Application.Interfaces
{
    public interface IMessageService
    {
        Task<bool> AddMessage(MessageEntity messageEntity);
        Task<MessageEntity> GetLastMessage(string senderUsername, string receiverUsername);
        Task<List<MessageEntity>> GetAllMessages(string senderUsername, string receiverUsername);
    }
}
