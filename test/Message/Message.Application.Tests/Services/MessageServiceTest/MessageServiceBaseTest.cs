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
        protected readonly Mock<IMessageRepository> mockMessageRepository;
        protected readonly Mock<IUserProvider> mockUserProvider;

        public MessageServiceBaseTest()
        {
            mockMessageRepository = new Mock<IMessageRepository>();
            mockUserProvider = new Mock<IUserProvider>();
        }
    }
}
