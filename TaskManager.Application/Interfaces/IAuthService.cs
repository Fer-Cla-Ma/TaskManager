using TaskManager.Application.DTOs.Auth;

namespace TaskManager.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> Register(RegisterRequest request);
        Task<AuthResult> LoginAsync(LoginRequest request);
    }
}
