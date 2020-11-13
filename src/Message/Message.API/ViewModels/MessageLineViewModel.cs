using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.ViewModels
{
    public class MessageLineViewModel
    {
        public string ReceiverUsername { get; set; }
        public string MessageLine { get; set; }

        public MessageLineViewModel()
        {
        }

        public MessageLineViewModel(string receiverUsername, string messageLine)
        {
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
