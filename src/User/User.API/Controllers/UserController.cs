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
        public async Task<ActionResult> PostRegister([FromBody] UserRegisterViewModel userRegisterViewModel)
        {
            var result = await userService.RegisterAsync(userRegisterViewModel.Username, userRegisterViewModel.Password, userRegisterViewModel.PasswordRepeat);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> PostLoginAsync([FromBody] UserViewModel userViewModel)
        {
            var userTokenModel = await userService.LoginAsync(userViewModel.Username, userViewModel.Password);
            return Ok(userTokenModel);
        }

        [HttpPost("block/{username}")]
        public async Task<ActionResult> PostBlockUserAsync(string username)
        {
            string currentUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            if (currentUsername == null)
            {
                return BadRequest();
            }

            var userTokenModel = await userService.BlockUserAsync(currentUsername, username);
            return Ok();
        }
    }
}
