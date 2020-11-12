using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.ViewModels
{
    public class MessageLineViewModel
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string MessageLine { get; set; }

        public MessageLineViewModel()
        {
        }

        public MessageLineViewModel(string senderUsername, string receiverUsername, string messageLine)
        {
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            MessageLine = messageLine;
        }

        public class MessageLineViewModelValidator : AbstractValidator<MessageLineViewModel>
        {
            public MessageLineViewModelValidator()
            {
                RuleFor(x => x.ReceiverUsername).NotEmpty();
                RuleFor(x => x.MessageLine).NotEmpty();
            }
        }

    }
}
