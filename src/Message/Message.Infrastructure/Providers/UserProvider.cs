using Message.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Providers
{
    public class UserProvider : IUserProvider
    {
        public Task<bool> IsUserBlocked(string userBy, string questionedUser)
        {
            return Task.FromResult(false);
        }

        public Task<bool> IsUserRegistered(string username)
        {
            return Task.FromResult(true);
        }
    }
}
