using Message.Application.Constants;
using Message.Application.Interfaces;
using Message.Core.Entities;
using Message.Core.Providers;
using Message.Core.Repositories;
using Message.Core.Results;
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

        public async Task<IResult> AddMessage(string messageLine, string senderUsername, string receiverUsername)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(receiverUsername);
            if (!isReceiverExist)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var isReceiverBlocked = await userProvider.IsBlockedByUser(receiverUsername);
            if (isReceiverBlocked)
            {
                return new ErrorResult(Messages.UserBlocked);
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(senderUsername, receiverUsername);
            messageQueue.MessageLines.Enqueue(messageLine);
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);

            await AddMessageToHistory(messageLine, senderUsername, receiverUsername);

            return new SuccessResult(Messages.MessageSended);
        }

        private async Task AddMessageToHistory(string messageLine, string senderUsername, string receiverUsername)
        {
            var messageEntity = new MessageEntity(senderUsername, receiverUsername, messageLine, DateTime.Now);
            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(senderUsername, receiverUsername);

            messageHistory.MessageEntities.Add(messageEntity);

            await this.messageHistoryRepository.UpdateMessageHistory(messageHistory);
        }

        public async Task<IDataResult<MessageHistroy>> GetMessageHistory(string who, string toWhom)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(toWhom);
            if (!isReceiverExist)
            {
                return new ErrorDataResult<MessageHistroy>(Messages.UserNotFound);
            }

            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(who, toWhom);
            return new SuccessDataResult<MessageHistroy>(messageHistory);
        }

        public async Task<IDataResult<string>> GetLastMessage(string senderUsername, string receiverUsername)
        {
            var isSenderRegistered = await userProvider.IsUserRegistered(senderUsername);
            if (!isSenderRegistered)
            {
                return new ErrorDataResult<string>(Messages.UserNotFound);
            }

            var isSenderBlocked = await userProvider.IsBlockedByUser(receiverUsername);
            if (isSenderBlocked)
            {
                return new ErrorDataResult<string>(Messages.UserBlocked);
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(senderUsername, receiverUsername);
            if (messageQueue.MessageLines.Count == 0)
            {
                return new ErrorDataResult<string>(Messages.NoMessage);
            }

            var messageLine = messageQueue.MessageLines.Dequeue();
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);

            return new SuccessDataResult<string>(messageLine);
        }
    }
}
