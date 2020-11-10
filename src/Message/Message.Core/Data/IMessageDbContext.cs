using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Data
{
    public interface IMessageDbContext
    {
        IDatabase Redis { get; }
    }
}
