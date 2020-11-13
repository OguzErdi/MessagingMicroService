using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.ViewModels
{
    public class MessageEntityViewModel
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string MessageLine { get; set; }
        public string Time { get; set; }

        public MessageEntityViewModel()
        {
        }
    }
}
