using Microsoft.AspNetCore.Mvc;
using WebApplication1.Requests;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        public readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(CreateUserRequest createUserRequest)
        {
            var success = _authService.Register(createUserRequest);
            if (!success)
                return BadRequest("Пользователь уже существует");

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var token = _authService.Login(request.Name, request.Password);

            if (token == null)
                return Unauthorized("Неверный логин или пароль");

            return Ok(new { token });
        }
    }
}
