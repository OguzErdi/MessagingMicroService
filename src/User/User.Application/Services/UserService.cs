using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Application.Constants;
using User.Application.Helpers;
using User.Application.Interfaces;
using User.Application.Models;
using User.Core.Entities;
using User.Core.Repositories;
using User.Core.Results;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private const int MinUsernameLength = 3;
        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
        }

        public async Task<IDataResult<UserTokenModel>> LoginAsync(string username, string password)
        {
            var user = await userRepository.GetUserAsync(username, password);
            if (user == null) 
                return new ErrorDataResult<UserTokenModel>(Messages.UserNotFound);

            var isPasswordCorrect = userRepository.VerifyPassword(user, password);
            if (!isPasswordCorrect)
                return new ErrorDataResult<UserTokenModel>(Messages.PasswordError);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string generatedToken = tokenHandler.WriteToken(token);

            return new SuccessDataResult<UserTokenModel>(new UserTokenModel(username, generatedToken), Messages.AccessTokenCreated);
        }

        public async Task<IResult> BlockUserAsync(string username, string blockedUsername)
        {
            var isBlockedUserExist = await userRepository.IsUserExistAsync(blockedUsername);
            if (!isBlockedUserExist)
                return new ErrorResult(Messages.UserNotFound);

            var isUserBlocked = await userRepository.BlockUserAsync(username, blockedUsername);
            if (!isUserBlocked)
            {
                return new ErrorResult(Messages.UserBlockedError);
            }

            return new SuccessResult(Messages.UserBlockedSuccesfully);
        }

        public async Task<IResult> RegisterAsync(string username, string password, string passworRepeat)
        {
            if (password != passworRepeat)
            {
                return new ErrorResult(Messages.PasswordsDoesntMatch);
            }

            var isUsernameAlreadyTeken = await userRepository.IsUserExistAsync(username);
            if (isUsernameAlreadyTeken)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }

            var isUsernameShort = username.Length < MinUsernameLength;
            if (isUsernameShort)
            {
                return new ErrorResult(string.Format(Messages.UsernameMustBeAtLeast, MinUsernameLength.ToString()));
            }

            var userEntity = await userRepository.AddUserAsync(username, password);
            if (userEntity == null)
            {
                return new ErrorResult(Messages.UsernameMustBeAtLeast);
            }

            return new SuccessResult(Messages.UserRegistered);
        }
    }
}
