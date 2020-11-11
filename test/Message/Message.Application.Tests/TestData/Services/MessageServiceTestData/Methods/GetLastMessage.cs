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
                yield return new object[] { new MessageEntity() { SenderUsername = Constants.SenderUser, ReceiverUsername = Constants.RecieverUser} };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ThereIsNotUnSendedMessage_ReturnNull : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new MessageEntity() { SenderUsername = Constants.SenderUser, ReceiverUsername = Constants.JustRegisteredUser, Content = "Test mesajı attım, geldi mi?" } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
