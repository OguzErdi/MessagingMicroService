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
        private readonly IMessageQueueRepository messageQueueRepository;
        private readonly IMessageHistoryRepository messageHistoryRepository;
        private readonly IUserProvider userProvider;

        public MessageService(
            IMessageQueueRepository messageQueueRepository,
            IMessageHistoryRepository messageHistoryRepository,
            IUserProvider userProvider)
        {
            this.messageQueueRepository = messageQueueRepository;
            this.messageHistoryRepository = messageHistoryRepository;
            this.userProvider = userProvider;
        }

        public async Task<bool> AddMessage(string messageLine, string senderUsername, string receiverUsername)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(receiverUsername);
            if (!isReceiverExist)
            {
                return false;
            }

            var isReceiverBlocked = await userProvider.IsUserBlocked(senderUsername, receiverUsername);
            if (isReceiverBlocked)
            {
                return false;
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(senderUsername, receiverUsername);
            messageQueue.MessageLines.Enqueue(messageLine);
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);
            
            await AddMessageToHistory(messageLine, senderUsername, receiverUsername);

            return true;
        }

        private async Task AddMessageToHistory(string messageLine, string senderUsername, string receiverUsername)
        {
            var messageEntity = new MessageEntity(senderUsername, receiverUsername, messageLine, DateTime.Now);
            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(senderUsername, receiverUsername);

            messageHistory.MessageEntities.Add(messageEntity);

            await this.messageHistoryRepository.UpdateMessageHistory(messageHistory); 
        }

        public async Task<MessageHistroy> GetMessageHistory(string who, string toWhom)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(toWhom);
            if (!isReceiverExist)
            {
                return null;
            }

            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(who, toWhom);
            return messageHistory;
        }

        public async Task<string> GetLastMessage(string senderUsername, string receiverUsername)
        {
            var isSenderRegistered = await userProvider.IsUserRegistered(senderUsername);
            if (!isSenderRegistered)
            {
                return null;
            }

            var isSenderBlocked = await userProvider.IsUserBlocked(receiverUsername, senderUsername);
            if (isSenderBlocked)
            {
                return null;
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(senderUsername, receiverUsername);
            var messageLine = messageQueue.MessageLines.Dequeue();
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);

            return messageLine;
        }
    }
}
