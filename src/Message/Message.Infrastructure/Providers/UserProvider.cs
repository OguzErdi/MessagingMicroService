using Message.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Infrastructure.Providers
{
    public class UserProvider : IUserProvider
    {
        public bool IsUserBlocked(string senderUsername, string recieverUsernam)
        {
            throw new NotImplementedException();
        }
    }
}
