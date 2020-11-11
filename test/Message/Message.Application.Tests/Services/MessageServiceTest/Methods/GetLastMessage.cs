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
        //todo: mesajı queue şeklinde verecek bir yapı düşün.
        [Theory]
        [ClassData(typeof(ThereIsUnSendedMessage_ReturnLastMessage))]
        public async Task ThereIsUnSendedMessage_ReturnLastMessage(string senderUsername, string receiverUsername)
        {
            //arrange
            Queue<string> dummyMessageQueue = new Queue<string>(new[] { "mesaj 1", "mesaj 2" } );
            var actualMessageQueue = new MessageQueue(senderUsername, receiverUsername, dummyMessageQueue);

            mockMessageQueueRepository.Setup(x => x.GetMessageQueue(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {   
                    return Task.FromResult(actualMessageQueue);
                });
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object);

            //act
            var result = await messageService.GetLastMessage(senderUsername, receiverUsername);

            //assert
            Assert.Equal(result.MessageLine, actualMessageQueue.MessageLines.Peek());
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ThereIsNotUnSendedMessage_ReturnNull))]
        public async Task ThereIsNotUnSendedMessage_ReturnNull(string senderUsername, string receiverUsername)
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
            var result = await messageService.GetLastMessage(senderUsername, receiverUsername);

            //assert
            Assert.Null(result);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
