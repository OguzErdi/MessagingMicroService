using Message.Application.Constants;
using Message.Application.Services;
using Message.Core.Entities;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xunit;
using static Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods.AddMessage;

namespace Message.Application.Tests.Services.MessageServiceTest.Methods
{
    public class AddMessage : MessageServiceBaseTest
    {
        [Theory]
        [ClassData(typeof(UnblockedUser_ReturnTrue))]
        public async Task UnblockedUser_ReturnTrue(string messageLine, string senderUsername, string receiverUsername)
        {
            //arrange
            mockMessageQueueRepository.Setup(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string senderUserName, string receiverUsername) =>
            {
                var messageQueue = new MessageQueue(Constants.MessageQueueKey, senderUserName, receiverUsername, Constants.MessageLines);
                return Task.FromResult(messageQueue);
            });

            mockMessageQueueRepository.Setup(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>())).ReturnsAsync(true);

            var messageHistory = new MessageHistroy(Constants.MessageHistoryKey, new List<string>() { senderUsername, receiverUsername }, Constants.MessageEntities);
            mockMessageHistoryRepository.Setup(x => x.GetMessageHistory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    return Task.FromResult(messageHistory);
                });

            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsBlockedByUser(It.IsAny<string>())).ReturnsAsync(false);
            
            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object,
                logger.Object);

            //act
            var result = await messageService.AddMessage(messageLine, senderUsername, receiverUsername);

            //assert
            Assert.True(result.Success);
            Assert.Equal(Messages.MessageSended, result.Message);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Once);
            mockMessageQueueRepository.Verify(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsBlockedByUser(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(BlockedUser_ReturnFalse))]
        public async Task BlockedUser_ReturnFalse(string messageLine, string senderUsername, string receiverUsername)
        {
            //arrange
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsBlockedByUser(It.IsAny<string>())).ReturnsAsync(true);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object,
                logger.Object);

            //act
            var result = await messageService.AddMessage(messageLine, senderUsername, receiverUsername);

            //assert
            Assert.False(result.Success);
            Assert.Equal(Messages.UserBlocked, result.Message);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Never);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Setup(x => x.IsBlockedByUser(It.IsAny<string>())).ReturnsAsync(false);
        }

        [Theory]
        [ClassData(typeof(UnRegisteredRecieverUser_ReturnFalse))]
        public async Task UnRegisteredRecieverUser_ReturnFalse(string messageLine, string senderUsername, string receiverUsername)
        {
            //arrange
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(false);
            mockUserProvider.Setup(x => x.IsBlockedByUser(It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object,
                logger.Object);

            //act
            var result = await messageService.AddMessage(messageLine, senderUsername, receiverUsername);

            //assert
            Assert.False(result.Success);
            Assert.Equal(Messages.UserNotFound, result.Message);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Never);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Setup(x => x.IsBlockedByUser(It.IsAny<string>())).ReturnsAsync(false);
        }
    }
}
