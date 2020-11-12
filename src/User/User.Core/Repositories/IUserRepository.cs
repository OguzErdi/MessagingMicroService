using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Core.Entities;

namespace User.Core.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserAsync(string username, string password);
        Task<bool> IsUserExistAsync(string username);
        Task<bool> BlockUserAsync(string username, string blcokedUsername);
        Task<UserEntity> AddUserAsync(string username, string password);
    }
}
