using Message.Core.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using User.Application.Helpers;

namespace Message.Infrastructure.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly AppSettings appSettings;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserProvider(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            this.appSettings = appSettings.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> IsBlockedByUser(string username)
        {
            using (var httpClient = new HttpClient())
            {
                var jwt = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

                using (var response = await httpClient.GetAsync(appSettings.UserApi + $"isblockedbyuser/{username}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var isBlocked = JsonConvert.DeserializeObject<bool>(apiResponse);
                    return isBlocked;
                }
            }
        }

        public async Task<bool> IsUserRegistered(string username)
        {
            using (var httpClient = new HttpClient())
            {
                var jwt = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt);

                using (var response = await httpClient.GetAsync(appSettings.UserApi + $"isexist/{username}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var isExist = JsonConvert.DeserializeObject<bool>(apiResponse);
                    return isExist;
                }
            }
        }
    }
}
