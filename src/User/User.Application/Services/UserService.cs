using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Application.Helpers;
using User.Application.Interfaces;
using User.Application.Models;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
        }

        public async Task<UserTokenModel> LoginAsync(string username, string password)
        {
            var user = await userRepository.GetUserAsync(username, password);

            if (user == null)
                return null;

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

            return new UserTokenModel(username, generatedToken);
        }

        public async Task<bool> BlockUserAsync(string username, string blockedUsername)
        {
            var isBlockedUserExist = await userRepository.IsUserExistAsync(blockedUsername);

            if (!isBlockedUserExist)
                return false;

            var isUserBloced = await userRepository.BlockUserAsync(username, blockedUsername);

            return isUserBloced;
        }

        public async Task<bool> RegisterAsync(string username, string password, string passworRepeat)
        {
            if (password != passworRepeat)
            {
                return false;
            }

            var userEntity = await userRepository.AddUserAsync(username, password);
            return userEntity != null;
        }
    }
}
