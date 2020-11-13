using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Application.Models;
using User.Core.Entities;
using User.Core.Results;

namespace User.Application.Interfaces
{
    public interface IUserService
    {
        Task<IDataResult<UserTokenModel>> LoginAsync(string username, string password);
        Task<IDataResult<bool>> IsBlockedUser(string byUser, string blockedUsername);
        Task<IDataResult<bool>> IsUserExist(string username);
        Task<IResult> RegisterAsync(string username, string password, string passworRepeat);

    }
}
