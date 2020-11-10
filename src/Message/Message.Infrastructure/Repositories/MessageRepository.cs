using Message.Core.Data;
using Message.Core.Entities;
using Message.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageDbContext dbContext;

        public MessageRepository(IMessageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddMessage(string senderUsername, string recieverUsername, string content)
        {
            throw new NotImplementedException();
        }

        public MessageEntity GetLastMessage(string senderUsername, string recieverUsername)
        {
            throw new NotImplementedException();
        }

        public List<MessageEntity> GetMessages(string senderUsername, string recieverUsername)
        {
            throw new NotImplementedException();
        }
    }
}
