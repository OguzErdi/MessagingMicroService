using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using User.API.ViewModel;
using User.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
            logger.LogInformation("Hello first log in UserController Constructor");
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> PostRegister([FromBody] UserRegisterViewModel userRegisterViewModel)
        {
            var result = await userService.RegisterAsync(userRegisterViewModel.Username, userRegisterViewModel.Password, userRegisterViewModel.PasswordRepeat);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> PostLoginAsync([FromBody] UserViewModel userViewModel)
        {
            var result = await userService.LoginAsync(userViewModel.Username, userViewModel.Password);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Block/{username}")]
        public async Task<IActionResult> PostBlockUserAsync(string username)
        {
            string currentUsername = User.FindFirst(ClaimTypes.Name).Value;
            var result = await userService.IsBlockedUser(currentUsername, username);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("IsBlockedUser/{username}")]
        public async Task<IActionResult> GetIsBlockedUser(string username)
        {
            string currentUsername = User.FindFirst(ClaimTypes.Name).Value;
            var result = await userService.IsBlockedUser(currentUsername, username);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("IsBlockedByUser/{username}")]
        public async Task<IActionResult> GetIsBlockedByUser(string username)
        {
            string currentUsername = User.FindFirst(ClaimTypes.Name).Value;
            var result = await userService.IsBlockedUser(username, currentUsername);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("IsExist/{username}")]
        public async Task<IActionResult> GetIsUserExist(string username)
        {
            var result = await userService.IsUserExist(username);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
