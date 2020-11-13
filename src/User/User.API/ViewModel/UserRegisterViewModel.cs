using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.ViewModel
{
    public class UserRegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }

        public class UserRegisterViewModelValidator : AbstractValidator<UserRegisterViewModel>
        {
            public UserRegisterViewModelValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.PasswordRepeat).NotEmpty();
            }
        }

    }
}
