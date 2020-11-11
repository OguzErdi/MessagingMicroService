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
            var actualMessageList = new List<MessageEntity>();
            actualMessageList.Add(new MessageEntity(senderUsername, receiverUsername, "mesaj"));
            actualMessageList.Add(new MessageEntity(senderUsername, receiverUsername, "mesaj"));
            actualMessageList.Add(new MessageEntity(senderUsername, receiverUsername, "mesaj"));
            actualMessageList.Add(new MessageEntity(senderUsername, receiverUsername, "mesaj"));

            mockMessageRepository.Setup(x => x.GetMessages(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    return Task.FromResult(actualMessageList);
                });
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(this.mockMessageRepository.Object, this.mockUserProvider.Object);

            //act
            var result = await messageService.GetAllMessages(senderUsername, receiverUsername);

            //assert
            Assert.Equal(result, actualMessageList);
            mockMessageRepository.Verify(x => x.GetMessages(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ThereAreNoMessages_ReturnNull))]
        public async Task ThereAreNoMessages_ReturnNull(string senderUsername, string receiverUsername)
        {
            //arrange
            mockMessageRepository.Setup(x => x.GetMessages(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string senderUserName, string receiverUsername) =>
                {
                    return Task.FromResult<List<MessageEntity>>(null);
                });
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var messageService = new MessageService(this.mockMessageRepository.Object, this.mockUserProvider.Object);

            //act
            var result = await messageService.GetAllMessages(senderUsername, receiverUsername);

            //assert
            Assert.Null(result);
            mockMessageRepository.Verify(x => x.AddMessage(It.IsAny<MessageEntity>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
