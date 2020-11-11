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

        public async Task<bool> UpdateMessageHistory(MessageQueue messageHistory)
        {
            string messageHistoryJson = JsonConvert.SerializeObject(messageHistory);

            var messageHistoryKey = keyGenerator.GenerateForHistory(messageHistory.SenderUsername, messageHistory.ReceiverUsername);
            var isUpdated = await context.Redis.StringSetAsync(messageHistoryKey, messageHistoryJson);

            if (!isUpdated)
            {
                return false;
            }

            return true;
        }

        public async Task<MessageQueue> GetMessageHistory(string senderUsername, string receiverUsername)
        {
            var messageHistoryKey = keyGenerator.GenerateForQueue(senderUsername, receiverUsername);

            var messageHistoryJson = await context.Redis.StringGetAsync(messageHistoryKey);
            if (messageHistoryJson.IsNullOrEmpty)
            {
                return null;
            }

            var messageHistory = JsonConvert.DeserializeObject<MessageQueue>(messageHistoryJson);

            return messageHistory;
        }
    }
}
