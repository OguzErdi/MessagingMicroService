using Message.Core.Data;
using Message.Core.Entities;
using Message.Core.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Repositories
{
    public class MessageHistoryRepository : IMessageHistoryRepository
    {
        private readonly IMessageDbContext context;
        private readonly IKeyGenerator keyGenerator;

        public MessageHistoryRepository(IMessageDbContext context, IKeyGenerator keyGenerator)
        {
            this.context = context;
            this.keyGenerator = keyGenerator;
        }

        public async Task<bool> UpdateMessageHistory(MessageHistroy messageHistory)
        {
            string messageHistoryJson = JsonConvert.SerializeObject(messageHistory);

            var isUpdated = await context.Redis.StringSetAsync(messageHistory.MessageHistoryKey, messageHistoryJson);

            if (!isUpdated)
            {
                return false;
            }

            return true;
        }

        public async Task<MessageHistroy> GetMessageHistory(string who, string toWhom)
        {
            var messageHistoryKey = keyGenerator.GenerateForMessageHistory(who, toWhom);

            var messageHistoryJson = await context.Redis.StringGetAsync(messageHistoryKey);
            if (messageHistoryJson.IsNullOrEmpty)
            {
                var participants = new List<string>() { who, toWhom};
                return new MessageHistroy(messageHistoryKey, participants, new List<MessageEntity>());
            }

            var messageHistory = JsonConvert.DeserializeObject<MessageHistroy>(messageHistoryJson);

            return messageHistory;
        }
    }
}
