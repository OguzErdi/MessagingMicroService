using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Core.Providers
{
    public interface IUserProvider
    {
        Task<bool> IsBlockedByUser(string username);
        Task<bool> IsUserRegistered(string username);
    }
}
