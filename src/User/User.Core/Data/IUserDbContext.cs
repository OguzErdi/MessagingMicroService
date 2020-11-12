using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Core.Data
{
    public interface IUserDbContext
    {
        IDatabase Redis { get; }
    }
}
