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

        public async Task<bool> AddMessage(MessageEntity messageEntity)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(messageEntity.ReceiverUsername);
            if (!isReceiverExist)
            {
                return false;
            }

            var isReceiverBlocked = await userProvider.IsUserBlocked(messageEntity.SenderUsername, messageEntity.ReceiverUsername);
            if (isReceiverBlocked)
            {
                return false;
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(messageEntity.SenderUsername, messageEntity.ReceiverUsername);
            messageQueue.MessageLines.Enqueue(messageEntity.MessageLine);
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);
            
            await AddMessageToHistory(messageEntity);

            return true;
        }

        private async Task AddMessageToHistory(MessageEntity messageEntity)
        {
            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(messageEntity.SenderUsername, messageEntity.ReceiverUsername);
            messageHistory.MessageLines.Enqueue(messageEntity.MessageLine);
            await this.messageHistoryRepository.UpdateMessageHistory(messageHistory);
        }

        public async Task<MessageQueue> GetMessageHistory(string senderUsername, string receiverUsername)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(receiverUsername);
            if (!isReceiverExist)
            {
                return null;
            }

            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(senderUsername, receiverUsername);
            return messageHistory;
        }

        public async Task<MessageEntity> GetLastMessage(string senderUsername, string receiverUsername)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(receiverUsername);
            if (!isReceiverExist)
            {
                return null;
            }

            var isReceiverBlocked = await userProvider.IsUserBlocked(senderUsername, receiverUsername);
            if (isReceiverBlocked)
            {
                return null;
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(senderUsername, receiverUsername);
            var messageLine = messageQueue.MessageLines.Dequeue();
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);

            var messageEntity = new MessageEntity(senderUsername, receiverUsername, messageLine);

            return messageEntity;
        }
    }
}
