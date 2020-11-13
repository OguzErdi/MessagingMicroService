using Message.Application.Constants;
using Message.Application.Services;
using Message.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods.GetLastMessage;

namespace Message.Application.Tests.Services.MessageServiceTest.Methods
{
    public class GetLastMessage : MessageServiceBaseTest
    {
        [Theory]
        [ClassData(typeof(ThereIsUnSendedMessage_ReturnLastMessage))]
        public async Task ThereIsUnSendedMessage_ReturnLastMessage(string senderUsername, string receiverUsername)
        {
            //arrange
            mockMessageQueueRepository.Setup(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    var messageQueue = new MessageQueue(Constants.MessageQueueKey, senderUserName, receiverUsername, Constants.MessageLines);
                    return Task.FromResult(messageQueue);
                });

            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsBlockedByUser(It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object,
                logger.Object);

            //act
            var result = await messageService.GetLastMessage(senderUsername, receiverUsername);

            //assert
            Assert.True(result.Success);
            Assert.Equal(Constants.MessageLine1, result.Data);
            Assert.Equal(Messages.GetMessageFromQueue, result.Message);
            mockMessageQueueRepository.Verify(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsBlockedByUser(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ThereIsNotUnSendedMessage_ReturnNull))]
        public async Task ThereIsNotUnSendedMessage_ReturnNull(string senderUsername, string receiverUsername)
        {
            //arrange
            mockMessageQueueRepository.Setup(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    var messageQueue = new MessageQueue(Constants.MessageQueueKey, senderUserName, receiverUsername, new Queue<string>());
                    return Task.FromResult(messageQueue);
                });

            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsBlockedByUser(It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object,
                logger.Object);

            //act
            var result = await messageService.GetLastMessage(senderUsername, receiverUsername);

            //assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(Messages.NoMessage, result.Message);
            mockMessageQueueRepository.Verify(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsBlockedByUser(It.IsAny<string>()), Times.Once);
        }
    }
}
