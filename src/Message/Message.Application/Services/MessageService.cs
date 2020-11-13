using Message.Application.Constants;
using Message.Application.Interfaces;
using Message.Core.Entities;
using Message.Core.Providers;
using Message.Core.Repositories;
using Message.Core.Results;
using Microsoft.Extensions.Logging;
using Serilog;
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
        private readonly ILogger<MessageService> logger;

        public MessageService(
            IMessageQueueRepository messageQueueRepository,
            IMessageHistoryRepository messageHistoryRepository,
            IUserProvider userProvider,
            ILogger<MessageService> logger)
        {
            this.messageQueueRepository = messageQueueRepository;
            this.messageHistoryRepository = messageHistoryRepository;
            this.userProvider = userProvider;
            this.logger = logger;
        }

        public async Task<IResult> AddMessage(string messageLine, string senderUsername, string receiverUsername)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(receiverUsername);
            if (!isReceiverExist)
            {
                logger.LogInformation(Messages.UserNotFound);
                return new ErrorResult(Messages.UserNotFound);
            }

            var isReceiverBlocked = await userProvider.IsBlockedByUser(receiverUsername);
            if (isReceiverBlocked)
            {
                logger.LogInformation(Messages.UserBlocked);
                return new ErrorResult(Messages.UserBlocked);
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(senderUsername, receiverUsername);
            messageQueue.MessageLines.Enqueue(messageLine);
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);

            await AddMessageToHistory(messageLine, senderUsername, receiverUsername);

            logger.LogInformation(Messages.MessageSended);
            return new SuccessResult(Messages.MessageSended);
        }

        private async Task AddMessageToHistory(string messageLine, string senderUsername, string receiverUsername)
        {
            var messageEntity = new MessageEntity(senderUsername, receiverUsername, messageLine, DateTime.Now);
            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(senderUsername, receiverUsername);

            messageHistory.MessageEntities.Add(messageEntity);
            logger.LogInformation(Messages.MessageAddedToHistory);

            await this.messageHistoryRepository.UpdateMessageHistory(messageHistory);
        }

        public async Task<IDataResult<MessageHistroy>> GetMessageHistory(string who, string toWhom)
        {
            var isReceiverExist = await userProvider.IsUserRegistered(toWhom);
            if (!isReceiverExist)
            {
                logger.LogInformation(Messages.UserNotFound);
                return new ErrorDataResult<MessageHistroy>(Messages.UserNotFound);
            }

            var messageHistory = await this.messageHistoryRepository.GetMessageHistory(who, toWhom);

            logger.LogInformation(Messages.GetMessageHistory);
            return new SuccessDataResult<MessageHistroy>(messageHistory, Messages.GetMessageHistory);
        }

        public async Task<IDataResult<string>> GetLastMessage(string senderUsername, string receiverUsername)
        {
            var isSenderRegistered = await userProvider.IsUserRegistered(senderUsername);
            if (!isSenderRegistered)
            {
                logger.LogInformation(Messages.UserNotFound);
                return new ErrorDataResult<string>(Messages.UserNotFound);
            }

            var isSenderBlocked = await userProvider.IsBlockedByUser(receiverUsername);
            if (isSenderBlocked)
            {
                logger.LogInformation(Messages.UserBlocked);
                return new ErrorDataResult<string>(Messages.UserBlocked);
            }

            var messageQueue = await this.messageQueueRepository.GetMessageQueue(senderUsername, receiverUsername);
            if (messageQueue.MessageLines.Count == 0)
            {
                logger.LogInformation(Messages.NoMessage);
                return new ErrorDataResult<string>(Messages.NoMessage);
            }

            var messageLine = messageQueue.MessageLines.Dequeue();
            await this.messageQueueRepository.UpdateMessageQueue(messageQueue);

            logger.LogInformation(Messages.GetMessageFromQueue);
            return new SuccessDataResult<string>(messageLine, Messages.GetMessageFromQueue);
        }
    }
}
