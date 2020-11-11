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
    public class MessageQueueRepository : IMessageQueueRepository
    {
        private readonly IMessageDbContext context;
        private readonly IKeyGenerator keyGenerator;

        public MessageQueueRepository(IMessageDbContext context, IKeyGenerator keyGenerator)
        {
            this.context = context;
            this.keyGenerator = keyGenerator;
        }

        public async Task<bool> UpdateMessageQueue(MessageQueue messageQueue)
        {
            string messageQueueJson = JsonConvert.SerializeObject(messageQueue);

            var messageQueueKey = keyGenerator.GenerateForQueue(messageQueue.SenderUsername, messageQueue.ReceiverUsername);
            var isUpdated = await context.Redis.StringSetAsync(messageQueueKey, messageQueueJson);

            if (!isUpdated)
            {
                return false;
            }

            return true;
        }


        public async Task<MessageQueue> GetMessageQueue(string senderUsername, string receiverUsername)
        {
            var messageQueueKey = keyGenerator.GenerateForQueue(senderUsername, receiverUsername);

            var messageQueueJson = await context.Redis.StringGetAsync(messageQueueKey);
            if (messageQueueJson.IsNullOrEmpty)
            {
                return null;
            }

            var messageQueue = JsonConvert.DeserializeObject<MessageQueue>(messageQueueJson);

            return messageQueue;
        }
    }
}
