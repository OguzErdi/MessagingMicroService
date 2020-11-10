using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Entities
{
    public class MessageEntity
    {
        public string SenderUsername { get; set; }
        public string RecieverUsername { get; set; }
        public string Content { get; set; }
    }
}
