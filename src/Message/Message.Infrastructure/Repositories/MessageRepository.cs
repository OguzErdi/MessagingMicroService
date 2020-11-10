using Message.Core.Data;
using Message.Core.Entities;
using Message.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageDbContext dbContext;

        public MessageRepository(IMessageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> AddMessage(MessageEntity messageEntity)
        {
            throw new NotImplementedException();
        }

        public Task<MessageEntity> GetLastMessage(string senderUsername, string recieverUsername)
        {
            throw new NotImplementedException();
        }

        public Task<List<MessageEntity>> GetMessages(string senderUsername, string recieverUsername)
        {
            throw new NotImplementedException();
        }
    }
}
