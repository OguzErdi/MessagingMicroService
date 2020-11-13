using System;
using System.Collections.Generic;
using System.Text;

namespace User.Core.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
