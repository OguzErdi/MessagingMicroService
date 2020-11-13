using Message.Application.Constants;
using Message.Application.Services;
using Message.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods.GetMessageHistory;

namespace Message.Application.Tests.Services.MessageServiceTest.Methods
{
    public class GetMessageHistory : MessageServiceBaseTest
    {

        [Theory]
        [ClassData(typeof(ThereAreMessages_ReturnMessageList))]
        public async Task ThereAreMessages_ReturnMessageList(string who, string toWhom)
        {
            //arrange
            var messageHistory = new MessageHistroy(Constants.MessageHistoryKey, new List<string>() { who, toWhom }, Constants.MessageEntities);

            mockMessageHistoryRepository.Setup(x => x.GetMessageHistory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    return Task.FromResult(messageHistory);
                });

            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object,
                logger.Object);

            //act
            var result = await messageService.GetMessageHistory(who, toWhom);

            //assert
            Assert.True(result.Success);
            Assert.Equal(Messages.GetMessageHistory, result.Message);
            Assert.Equal(messageHistory, result.Data);
            mockMessageHistoryRepository.Verify(x => x.GetMessageHistory(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ThereAreNoMessages_ReturnNull))]
        public async Task ThereAreNoMessages_ReturnNull(string who, string toWhom)
        {
            //arrange
            var messageHistory = new MessageHistroy(Constants.MessageHistoryKey, new List<string>() { who, toWhom }, new List<MessageEntity>());

            mockMessageHistoryRepository.Setup(x => x.GetMessageHistory(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    return Task.FromResult(messageHistory);
                });

            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object,
                logger.Object);

            //act
            var result = await messageService.GetMessageHistory(who, toWhom);

            //assert
            Assert.True(result.Success);
            Assert.Equal(Messages.GetMessageHistory, result.Message);
            Assert.Equal(messageHistory, result.Data);
            mockMessageHistoryRepository.Verify(x => x.GetMessageHistory(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
        }
    }
}
