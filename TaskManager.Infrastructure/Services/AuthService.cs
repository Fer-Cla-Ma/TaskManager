using System.Security.Cryptography;
using System.Text;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure.Services
{
    public class AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) : IAuthService
    {
        public async Task<AuthResult> Register(RegisterRequest request)
        {
            var users = await userRepository.GetAllAsync();
            if (users.Any(u => u.Email == request.Email))
                throw new InvalidOperationException("The user already exists.");            
                               
            var user = new User
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };

            await userRepository.AddAsync(user);

            var token = jwtTokenGenerator.GenerateToken(user);

            return new AuthResult { Email = user.Email, Token = token };
        }        

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Email and password must not be empty.");

            var users = await userRepository.GetAllAsync();

            var user = users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null || user.PasswordHash != HashPassword(request.Password))
                throw new Exception("Invalid credentials.");

            var token = jwtTokenGenerator.GenerateToken(user);

            return new AuthResult { Email = user.Email, Token = token };
        }


        private static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
