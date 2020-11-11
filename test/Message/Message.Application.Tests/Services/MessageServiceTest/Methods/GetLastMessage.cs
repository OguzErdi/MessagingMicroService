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
        public async Task ThereIsUnSendedMessage_ReturnLastMessage(string senderUsername, string recieverUsername)
        {
            //arrange
            var actualLastMessage = new MessageEntity(senderUsername, recieverUsername, "son mesaj");

            mockMessageRepository.Setup(x => x.GetMessage(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string recieverUsername) =>
                {   
                    return Task.FromResult(actualLastMessage);
                });
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(this.mockMessageRepository.Object, this.mockUserProvider.Object);

            //act
            var result = await messageService.GetLastMessage(senderUsername, recieverUsername);

            //assert
            Assert.Equal(result, actualLastMessage);
            mockMessageRepository.Verify(x => x.AddMessage(It.IsAny<MessageEntity>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ThereIsNotUnSendedMessage_ReturnNull))]
        public async Task ThereIsNotUnSendedMessage_ReturnNull(string senderUsername, string recieverUsername)
        {
            //arrange
            mockMessageRepository.Setup(x => x.GetMessage(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string recieverUsername) =>
                {
                    return Task.FromResult<MessageEntity>(null);
                });
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            
            var messageService = new MessageService(this.mockMessageRepository.Object, this.mockUserProvider.Object);

            //act
            var result = await messageService.GetLastMessage(senderUsername, recieverUsername);

            //assert
            Assert.Null(result);
            mockMessageRepository.Verify(x => x.AddMessage(It.IsAny<MessageEntity>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
