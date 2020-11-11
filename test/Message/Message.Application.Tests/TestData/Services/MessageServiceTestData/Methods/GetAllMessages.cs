using Message.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods
{
    public class GetAllMessages
    {
        public class ThereAreMessages_ReturnMessageList : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Constants.SenderUser, Constants.RecieverUser };

            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ThereAreNoMessages_ReturnNull : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Constants.SenderUser, Constants.JustRegisteredUser };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
