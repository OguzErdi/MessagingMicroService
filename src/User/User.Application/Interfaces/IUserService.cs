using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Application.Models;
using User.Core.Entities;

namespace User.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserTokenModel> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password, string passworRepeat);
        Task<bool> BlockUserAsync(string username, string blockedUsername);

    }
}
