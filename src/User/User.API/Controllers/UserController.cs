using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
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
        [HttpPost("login")]
        public async Task<IActionResult> PostLoginAsync([FromBody] UserViewModel userViewModel)
        {
            var result = await userService.LoginAsync(userViewModel.Username, userViewModel.Password);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("block/{username}")]
        public async Task<ActionResult> PostBlockUserAsync(string username)
        {
            string currentUsername = User.FindFirst(ClaimTypes.Name).Value;
            var result = await userService.BlockUserAsync(currentUsername, username);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
