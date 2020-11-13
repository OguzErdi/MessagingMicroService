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
        Task<IResult> BlockUserAsync(string username, string blockedUsername);
        Task<IResult> RegisterAsync(string username, string password, string passworRepeat);

    }
}
