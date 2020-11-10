using Message.Application.Interfaces;
using Message.Core.Entities;
using Message.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public bool AddMessage(string senderUsername, string recieverUsername, string content)
        {
            throw new NotImplementedException();
        }

        public MessageEntity GetMessage(string senderUsername, string recieverUsername)
        {
            throw new NotImplementedException();
        }
    }
}
