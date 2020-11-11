using Message.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Message.Application.Tests.TestData.Services.MessageServiceTestData.Methods
{
    class GetLastMessage
    {
        public class ThereIsUnSendedMessage_ReturnLastMessage : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Constants.SenderUser, Constants.RecieverUser };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ThereIsNotUnSendedMessage_ReturnNull : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { Constants.SenderUser, Constants.JustRegisteredUser };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
