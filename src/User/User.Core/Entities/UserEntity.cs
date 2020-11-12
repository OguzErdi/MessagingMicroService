using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace User.Core.Entities
{
    public class UserEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public List<string> BlockedUser { get; set; }

        public UserEntity(string username, string passwordHash, List<string> blockedUser)
        {
            Username = username;
            PasswordHash = passwordHash;
            BlockedUser = blockedUser;
        }
    }
}
