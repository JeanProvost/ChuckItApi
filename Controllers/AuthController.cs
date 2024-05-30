using System.Threading.Tasks;
using ChuckItApi.Models.DTOs.Auth;
using ChuckItApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChuckItApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.RegisterUserAsync(registerDto);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok(new { Message = "User registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authService.LoginUserAsync(loginDto);

            if(token == null)
            {
                return Unauthorized(new { Message = "Invalid login credentials" });
            }
            return Ok(new { Token = token });
        }
    }
}
