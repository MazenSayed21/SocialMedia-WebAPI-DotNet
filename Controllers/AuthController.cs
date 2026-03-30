using Microsoft.AspNetCore.Mvc;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using SOCIALIZE.DTOs;

namespace SOCIALIZE.Controllers
{
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]registerDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return result ? Ok("User registered successfully") : BadRequest("Email already exists");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]loginDTO dto)
        {
            var token = await _authService.LoginAsync(dto.email, dto.password);
            return token == null ? Unauthorized("Invalid credentials") : Ok(new { Token = token });
        }
    }
}
