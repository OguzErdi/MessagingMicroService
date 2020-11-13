using Message.Core.AuthorizationGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.AuthorizationGenerator
{
    public class BearerAuthorizationGenerator : IAuthorizationGenerator
    {
        private const string Bearer = "Bearer";
        private readonly IHttpContextAccessor httpContextAccessor;

        public BearerAuthorizationGenerator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public AuthenticationHeaderValue GetHeader()
        {
            var jwt = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace($"{Bearer} ", "");
            return new AuthenticationHeaderValue(Bearer, jwt);
        }
    }
}
