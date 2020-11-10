using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Providers
{
    public interface IUserProvider
    {
        bool IsUserBlocked(string senderUsername, string recieverUsernam);
    }
}
