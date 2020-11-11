using Message.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods
{
    public class AddMessage
    {
        public class UnblockedUser_ReturnTrue : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new MessageEntity() { SenderUsername = Constants.SenderUser, RecieverUsername = Constants.RecieverUser, Content = "Test mesajı attım, geldi mi?" } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class BlockedUser_ReturnFalse : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new MessageEntity() { SenderUsername = Constants.SenderUser, RecieverUsername = Constants.JustRegisteredUser, Content = "Test mesajı attım, geldi mi?" } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class UnRegisteredRecieverUser_ReturnFalse : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new MessageEntity() { SenderUsername = Constants.SenderUser, RecieverUsername = Constants.UnRegisteredUser, Content = "Test mesajı attım, geldi mi?" } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
