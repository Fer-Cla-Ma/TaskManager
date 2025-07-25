using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Middleware;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController(
        IAuthService authService, 
        ILogger<ErrorHandlingMiddleware> logger) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var result = await authService.RegisterAsync(request);
                return Ok(result);
            }
            catch (ConflictException ex)
            {
                return Conflict(new { message = ex.Message }); // 409 Conflict si el usuario ya existe
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Registration error");
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await authService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await authService.RefreshTokenAsync(refreshToken);
            if(result == null)
            {
                return Unauthorized("Refresh token inválido o expirado.");
            }

            return Ok(result);
        }

    }
}
