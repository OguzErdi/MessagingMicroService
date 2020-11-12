using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using User.Core.Data;

namespace User.Infrastructer.Data
{
    public class UserDbContext : IUserDbContext
    {
        //to craete connection with redis db
        private readonly ConnectionMultiplexer redisConnection;

        //get ConnectionMultiplexer via Dependency Injection
        public UserDbContext(ConnectionMultiplexer redisConnection)
        {
            this.redisConnection = redisConnection;
            Redis = this.redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
