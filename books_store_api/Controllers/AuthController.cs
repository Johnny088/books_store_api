using books_store_api.Controllers.Extensions;
using books_store_BLL.Dtos.Auth;
using books_store_BLL.Dtos.Services;
using Microsoft.AspNetCore.Mvc;

namespace books_store_api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _JwtService;
        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _JwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return this.GetAction(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
           var response = await _authService.LoginAsync(dto);
            return this.GetAction(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] string refreshToken)
        {
            var response = await _JwtService.RefreshAsync(refreshToken);
            return this.GetAction(response);
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string uid, [FromQuery] string t)
        {
            var response = await _authService.ConfirmEmailAsync(uid, t);
            return this.GetAction(response);
        }
    
    }
}
