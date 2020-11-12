using System.Collections.Generic;

namespace Message.Infrastructure.Repositories
{
    public interface IKeyGenerator
    {
        string GenerateForMessageQueue(string senderUsername, string receiverUsername);
        string GenerateForMessageHistory(string who, string toWhom);
    }
}