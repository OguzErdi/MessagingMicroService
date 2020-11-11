using Message.Application.Interfaces;
using Message.Core.Entities;
using Message.Core.Providers;
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
        private readonly IUserProvider userProvider;

        public MessageService(IMessageRepository messageRepository, IUserProvider userProvider)
        {
            this.messageRepository = messageRepository;
            this.userProvider = userProvider;
        }

        public Task<bool> AddMessage(MessageEntity messageEntity)
        {
            throw new NotImplementedException();
        }

        public Task<MessageQueue> GetAllMessages(string senderUsername, string receiverUsername)
        {
            throw new NotImplementedException();
        }

        public Task<MessageEntity> GetLastMessage(string senderUsername, string receiverUsername)
        {
            throw new NotImplementedException();
        }
    }
}
