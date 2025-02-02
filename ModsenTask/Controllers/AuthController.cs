using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenTask.Application.Services;
using ModsenTask.Contracts.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace ModsenTask.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null) 
                return BadRequest();
            return Ok(result);
        }

        [HttpPost("refresh/{email}")]
        public async Task<IActionResult> Refresh(string email, [FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(email, refreshToken);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
    }
}
