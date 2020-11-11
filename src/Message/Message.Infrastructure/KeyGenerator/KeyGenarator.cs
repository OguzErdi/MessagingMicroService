using Message.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Message.Infrastructure.KeyGenerator
{
    public class KeyGenarator : IKeyGenerator
    {
        private const string History = "History";

        public string GenerateForQueue(string senderUsername, string receiverUsername)
        {
            string key = default;
            
            var orderedList = new List<string>() { senderUsername, receiverUsername };
            orderedList.OrderBy(x => x);
            
            string.Join(key, orderedList.ToArray());

            return key;
        }

        public string GenerateForHistory(string senderUsername, string receiverUsername)
        {
            string key = default;

            var orderedList = new List<string>() { senderUsername, receiverUsername };
            orderedList.OrderBy(x => x);

            string.Join(key, orderedList.ToArray());

            return key + History;
        }
    }
}
