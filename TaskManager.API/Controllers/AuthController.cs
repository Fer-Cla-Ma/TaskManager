using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Middleware;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Services;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController(IAuthService authService, ILogger<ErrorHandlingMiddleware> logger) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            try
            {
                var result = await authService.Register(request);

                if (result == null || string.IsNullOrWhiteSpace(result.Token))
                {
                    return BadRequest(new { message = "Registration failed." });
                }

                return Ok(result); 
            }
            catch (InvalidOperationException ex)
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
    }
}
