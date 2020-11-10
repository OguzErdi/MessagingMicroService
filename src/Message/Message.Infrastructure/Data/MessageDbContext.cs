using Message.Core.Data;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Infrastructure.Data
{
    public class MessageDbContext : IMessageDbContext
    {
        //to craete connection with redis db
        private readonly ConnectionMultiplexer redisConnection;

        //get ConnectionMultiplexer via Dependency Injection
        public MessageDbContext(ConnectionMultiplexer redisConnection)
        {
            this.redisConnection = redisConnection;
            Redis = this.redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
