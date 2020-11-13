using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.ViewModel
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class UserViewModelValidator : AbstractValidator<UserViewModel>
        {
            public UserViewModelValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
