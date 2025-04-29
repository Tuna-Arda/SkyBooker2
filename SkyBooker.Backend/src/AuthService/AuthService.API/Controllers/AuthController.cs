using Microsoft.AspNetCore.Mvc;
using AuthService.Application;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route(""api"")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost(""register"")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _auth.RegisterAsync(dto);
            return Ok();
        }

        [HttpGet(""login"")]
        public async Task<IActionResult> Login([FromQuery] LoginDto dto)
        {
            var token = await _auth.LoginAsync(dto);
            return Ok(new { token });
        }
    }
}
