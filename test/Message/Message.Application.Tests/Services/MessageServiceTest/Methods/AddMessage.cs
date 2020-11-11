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
            mockMessageQueueRepository.Setup(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            
            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object);

            //act
            var result = await messageService.AddMessage(messageEntity);

            //assert
            Assert.True(result);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(BlockedUser_ReturnFalse))]
        public async Task BlockedUser_ReturnFalse(MessageEntity messageEntity)
        {
            //arrange
            mockMessageQueueRepository.Setup(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            
            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object);

            //act
            var result = await messageService.AddMessage(messageEntity);

            //assert
            Assert.False(result);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(UnRegisteredRecieverUser_ReturnFalse))]
        public async Task UnRegisteredRecieverUser_ReturnFalse(MessageEntity messageEntity)
        {
            //arrange
            mockMessageQueueRepository.Setup(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>())).ReturnsAsync(true);
            mockUserProvider.Setup(x => x.IsUserRegistered(It.IsAny<string>())).ReturnsAsync(false);
            mockUserProvider.Setup(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            
            var messageService = new MessageService(
                mockMessageQueueRepository.Object,
                mockMessageHistoryRepository.Object,
                mockUserProvider.Object);

            //act
            var result = await messageService.AddMessage(messageEntity);

            //assert
            Assert.False(result);
            mockMessageQueueRepository.Verify(x => x.UpdateMessageQueue(It.IsAny<MessageQueue>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserRegistered(It.IsAny<string>()), Times.Once);
            mockUserProvider.Verify(x => x.IsUserBlocked(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
