﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Message.API.ViewModels
{
    public class MessageViewModel
    {
        public string SenderUsername { get; set; }
        public string RecieverUsername { get; set; }
        public string Content { get; set; }

        public class MessageViewModelValidator : AbstractValidator<MessageViewModel>
        {
            public MessageViewModelValidator()
            {
                RuleFor(x => x.SenderUsername).NotEmpty();
                RuleFor(x => x.RecieverUsername).NotEmpty();
                RuleFor(x => x.Content).NotEmpty();
            }
        }
    }
}