using System;
using System.Collections.Generic;
using System.Text;

namespace User.Core.Results
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
