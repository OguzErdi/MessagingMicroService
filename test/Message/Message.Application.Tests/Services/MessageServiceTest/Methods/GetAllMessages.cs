using Message.Application.Services;
using Message.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods.GetAllMessages;

namespace Message.Application.Tests.Services.MessageServiceTest.Methods
{
    public class GetAllMessages : MessageServiceBaseTest
    {

        [Theory]
        [ClassData(typeof(ThereAreMessages_ReturnMessageList))]
        public async Task ThereAreMessages_ReturnMessageList(string senderUsername, string receiverUsername)
        {
            //arrange
            Queue<string> messageLineList = new Queue<string>(new[] { "mesaj 1", "mesaj 2"});
            var actualMessageList = new MessageQueue(senderUsername, receiverUsername, messageLineList);

            mockMessageQueueRepository.Setup(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    return Task.FromResult(actualMessageList);
                });
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object);

            //act
            var result = await messageService.GetMessageHistory(senderUsername, receiverUsername);

            //assert
            Assert.Equal(result, actualMessageList);
            mockMessageQueueRepository.Verify(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ThereAreNoMessages_ReturnNull))]
        public async Task ThereAreNoMessages_ReturnNull(string senderUsername, string receiverUsername)
        {
            //arrange
            mockMessageQueueRepository.Setup(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    return Task.FromResult<MessageQueue>(null);
                });
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object);

            //act
            var result = await messageService.GetMessageHistory(senderUsername, receiverUsername);

            //assert
            Assert.Null(result);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
