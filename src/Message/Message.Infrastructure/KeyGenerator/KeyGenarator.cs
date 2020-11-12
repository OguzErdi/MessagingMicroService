using Message.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Message.Infrastructure.KeyGenerator
{
    public class KeyGenarator : IKeyGenerator
    {
        private const string History = "_MessageHistory";

        public string GenerateForMessageQueue(string senderUsername, string receiverUsername)
        {
            string key = $"from_{senderUsername}_to_{receiverUsername}_MessageQueue";
            return key;
        }

        public string GenerateForMessageHistory(string who, string toWhom)
        {
            string key = default;

            var nameList = new List<string>() { who, toWhom };
            var orderedList = nameList.OrderBy(x => x).ToArray();

            key = string.Join("_", orderedList.ToArray());

            return key + History;
        }
    }
}
