using FishMarketProjectDomain.IService;
using FishMarketProjectDomain.Models.Request;
using FishMarketProjectService.Services;
using Microsoft.AspNetCore.Mvc;

namespace fish_market_project.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Register a user
        /// </summary>
        /// <response code="200">User registered, email send</response>
        /// <response code="500">Error to register user</response>
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequest userRegister)
        {

            return Ok(true);
        }
    }
}
