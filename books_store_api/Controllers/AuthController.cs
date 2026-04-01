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
        public AuthController(AuthService authService)
        {
            _authService = authService;
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
    }
}
