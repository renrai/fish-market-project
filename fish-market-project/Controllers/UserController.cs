using fish_market_project.Authentication;
using FishMarketProjectDomain.IService;
using FishMarketProjectDomain.Models.Request;
using FishMarketProjectService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace fish_market_project.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JWT _token;
        public UserController(IUserService userService, JWT token)
        {
            _userService = userService;
            _token = token;
        }
        /// <summary>
        /// Register a user
        /// </summary>
        /// <response code="200">User registered, email send</response>
        /// <response code="500">Error to register user</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRequest userRegister)
        {
            var register = await _userService.RegisterUser(userRegister);
            if (register)
            {
                return Ok(register);

            }
            else
            {
                return BadRequest("Email already exists!");
            }
            return Ok(register);
        }
        /// <summary>
        /// Register a user
        /// </summary>
        /// <response code="200">User registered, email send</response>
        /// <response code="400">Invalid token or user</response>
        /// <response code="500">Error to register user</response>
        [HttpGet]
        [AllowAnonymous]
        [Route("validate-email")]
        public async Task<IActionResult> ValidateEmail([FromHeader] string email, [FromHeader] string token)
        {
            var validate = await _userService.ValidateToken(email, token);
            if (validate)
            {
                return Ok(validate);

            }
            else
            {
                return BadRequest("Invalid token or user email!");
            }
        }
        /// <summary>
        /// Resend Verification Email
        /// </summary>
        /// <response code="200">Email sended</response>
        /// <response code="400">Invalid user or already verified</response>
        /// <response code="500">Error to register user</response>
        [HttpGet]
        [AllowAnonymous]
        [Route("resend-email")]
        public async Task<IActionResult> ResendVerificationEmail([FromQuery] string email)
        {
            var validate = await _userService.ResendEmailVerification(email);
            if (validate)
            {
                return Ok(validate);

            }
            else
            {
                return BadRequest("Invalid email or user already verified!");
            }
        }
        /// <summary>
        /// Login user
        /// </summary>
        /// <response code="200">Login ok, return token</response>
        /// <response code="400">Invalid email or password or not verified</response>
        /// <response code="500">Error to login</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRegister)
        {
            var login = await _userService.Login(userRegister);
            if (login is false)
            {
                return BadRequest("Invalid email or password or email not verified!");

            }
            else
            {
                var token = _token.CreateToken(userRegister);
                return Ok(token);
            }
        }
    }
}
