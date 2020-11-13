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
                yield return new object[] { Constants.MessageLine1, Constants.SenderUser, Constants.RecieverUser };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class BlockedUser_ReturnFalse : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Constants.MessageLine2, Constants.SenderUser, Constants.RecieverUser };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class UnRegisteredRecieverUser_ReturnFalse : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Constants.MessageLine3, Constants.SenderUser, Constants.RecieverUser };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
