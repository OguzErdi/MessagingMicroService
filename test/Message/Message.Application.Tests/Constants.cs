using Message.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Application.Tests
{
    public class Constants
    {
        public static readonly string SenderUser = "SenderUser";
        public static readonly string RecieverUser = "RecieverUser";
        public static readonly string RecieverUserSecondary = "RecieverUserSecondary";
        public static readonly string JustRegisteredUser = "JustRegisteredUser";
        public static readonly string UnRegisteredUser = "UnRegisteredUser";

        public static readonly string MessageLine1 = "MessageLine 1";
        public static readonly string MessageLine2 = "MessageLine 2";
        public static readonly string MessageLine3 = "MessageLine 3";

        public static readonly string MessageQueueKey = "MessageQueueKey";
        public static readonly string MessageHistoryKey = "MessageHistoryKey";

        public static readonly Queue<string> MessageLines = new Queue<string>(new[] { MessageLine1, MessageLine2, MessageLine3 });
        public static readonly MessageEntity MessageEntity1 = new MessageEntity(SenderUser, RecieverUser, MessageLine1, DateTime.Now);
        public static readonly MessageEntity MessageEntity2 = new MessageEntity(SenderUser, RecieverUser, MessageLine2, DateTime.Now);
        public static readonly MessageEntity MessageEntity3 = new MessageEntity(SenderUser, RecieverUser, MessageLine3, DateTime.Now);

        public static readonly List<MessageEntity> MessageEntities = new List<MessageEntity>() { MessageEntity1, MessageEntity2, MessageEntity3 };
    }
}
