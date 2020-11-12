using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Core.Data;
using User.Core.Entities;
using User.Core.PasswordHasher;
using User.Core.Repositories;

namespace User.Infrastructer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDbContext context;
        private readonly IPasswordHasher passwordHasher;

        public UserRepository(IUserDbContext context, IPasswordHasher passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<UserEntity> AddUserAsync(string username, string password)
        {
            string passwordHash = passwordHasher.HashPassword(password);

            var userEntity = new UserEntity(username, passwordHash, new List<string>());

            string userEntityJson = JsonConvert.SerializeObject(userEntity);

            var isUpdated = await context.Redis.StringSetAsync(username, userEntityJson);

            return !isUpdated ? null : userEntity;
        }

        public async Task<bool> BlockUserAsync(string username, string blcokedUsername)
        {
            var userEntityJson = await context.Redis.StringGetAsync(username);
            var userEntity = JsonConvert.DeserializeObject<UserEntity>(userEntityJson);

            userEntity.BlockedUser.Add(blcokedUsername);

            var updatedUserEntityJson = JsonConvert.SerializeObject(userEntity);

            return await context.Redis.StringSetAsync(username, updatedUserEntityJson);
        }

        public async Task<UserEntity> GetUserAsync(string username, string password)
        {
            var userEntityJson = await context.Redis.StringGetAsync(username);
            if (userEntityJson.IsNullOrEmpty)
            {
                return null;
            }
            var userEntity = JsonConvert.DeserializeObject<UserEntity>(userEntityJson);

            var isPasswordCorrect = passwordHasher.VerifyPassword(password, userEntity.PasswordHash);

            return !isPasswordCorrect ? null : userEntity;
        }

        public async Task<bool> IsUserExistAsync(string username)
        {
            var userEntityJson = await context.Redis.StringGetAsync(username);
            return !userEntityJson.IsNullOrEmpty;
        }
    }
}
