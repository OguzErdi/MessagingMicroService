using Message.Application.Services;
using Message.Core.Entities;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods.AddMessage;

namespace Message.Application.Tests.Services.MessageServiceTest.Methods
{
    public class AddMessage : MessageServiceBaseTest
    {
        //todo: mesajı hem de sql e yazmalı belki de?
        [Theory]
        [ClassData(typeof(UnblockedUser_ReturnTrue))]
        public async Task UnblockedUser_ReturnTrue(MessageEntity messageEntity)
        {
            //arrange
            mockMessageRepository.Setup(x => x.AddMessage(It.IsAny<MessageEntity>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            var messageService = new MessageService(mockMessageRepository.Object, mockUserProvider.Object);

            //act
            var result = await messageService.AddMessage(messageEntity);

            //assert
            Assert.True(result);
            mockMessageRepository.Verify(x => x.AddMessage(It.IsAny<MessageEntity>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(BlockedUser_ReturnFalse))]
        public async Task BlockedUser_ReturnFalse(MessageEntity messageEntity)
        {
            //arrange
            mockMessageRepository.Setup(x => x.AddMessage(It.IsAny<MessageEntity>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            var messageService = new MessageService(mockMessageRepository.Object, mockUserProvider.Object);

            //act
            var result = await messageService.AddMessage(messageEntity);

            //assert
            Assert.False(result);
            mockMessageRepository.Verify(x => x.AddMessage(It.IsAny<MessageEntity>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(UnRegisteredRecieverUser_ReturnFalse))]
        public async Task UnRegisteredRecieverUser_ReturnFalse(MessageEntity messageEntity)
        {
            //arrange
            mockMessageRepository.Setup(x => x.AddMessage(It.IsAny<MessageEntity>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(false);
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            var messageService = new MessageService(mockMessageRepository.Object, mockUserProvider.Object);

            //act
            var result = await messageService.AddMessage(messageEntity);

            //assert
            Assert.False(result);
            mockMessageRepository.Verify(x => x.AddMessage(It.IsAny<MessageEntity>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
