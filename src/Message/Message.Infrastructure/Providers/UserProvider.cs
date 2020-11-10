using Message.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Providers
{
    public class UserProvider : IUserProvider
    {
        public Task<bool> IsUserBlocked(string senderUsername, string recieverUsernam)
        {
            throw new NotImplementedException();
        }
    }
}
