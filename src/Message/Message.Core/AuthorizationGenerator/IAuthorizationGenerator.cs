using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Message.Core.AuthorizationGenerator
{
    public interface IAuthorizationGenerator
    {
        AuthenticationHeaderValue GetHeader();
    }
}
