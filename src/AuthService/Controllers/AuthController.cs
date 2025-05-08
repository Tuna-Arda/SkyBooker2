using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, IJwtService jwtService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            _logger.LogInformation($"Registration attempt for username: {model.Username}");
            
            if (await _userService.UserExists(model.Username, model.Email))
                return BadRequest(new { message = "Username or Email already exists" });

            var user = await _userService.Register(model);

            if (user == null)
                return BadRequest(new { message = "User registration failed" });

            // Generate token
            var token = _jwtService.GenerateToken(user);

            // Return basic user info and token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation($"Login attempt for username: {model.Username}");
            
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            // Generate token
            var token = _jwtService.GenerateToken(user);

            // Return basic user info and token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            });
        }
    }
}
