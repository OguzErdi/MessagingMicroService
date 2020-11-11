using Message.Core.Providers;
using Message.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Application.Tests.Services.MessageServiceTest
{
    public abstract class MessageServiceBaseTest
    {
        protected readonly Mock<IMessageQueueRepository> mockMessageQueueRepository;
        protected readonly Mock<IMessageHistoryRepository> mockMessageHistoryRepository;
        protected readonly Mock<IUserProvider> mockUserProvider;

        public MessageServiceBaseTest()
        {
            mockMessageQueueRepository = new Mock<IMessageQueueRepository>();
            mockMessageHistoryRepository = new Mock<IMessageHistoryRepository>();
            mockUserProvider = new Mock<IUserProvider>();
        }
    }
}
