﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Core.Results
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
