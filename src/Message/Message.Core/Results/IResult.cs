using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
