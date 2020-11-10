using Message.Application.Interfaces;
using Message.Core.Entities;
using Message.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public Task<bool> AddMessage(MessageEntity messageEntity)
        {
            throw new NotImplementedException();
        }

        public Task<MessageEntity> GetMessage(string senderUsername, string recieverUsername)
        {
            throw new NotImplementedException();
        }
    }
}
