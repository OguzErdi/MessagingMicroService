using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Message.Core.Providers
{
    public interface IUserProvider
    {
        Task<bool> IsUserBlocked(string senderUsername, string recieverUsernam);
        Task<bool> IsUserRegistered(string username);
    }
}
