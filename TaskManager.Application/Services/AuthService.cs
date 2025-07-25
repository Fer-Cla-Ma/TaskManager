using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services
{
    public class AuthService(
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenGenerator jwtTokenGenerator
        ) : IAuthService
    {
        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var duplicate = result.Errors.Any(e =>
                e.Code == "DuplicateUserName" || e.Code == "DuplicateEmail");

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                if (duplicate)
                {
                    throw new ConflictException("User already exists.");
                }
                throw new Exception($"Registration failed: {errors}");
            }

            return new AuthResult
            {
                Email = user.Email,
                AccessToken = jwtTokenGenerator.CreateAccessToken(user),
                RefreshToken = jwtTokenGenerator.CreateRefreshToken()
            };
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid Credentials.");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid Credentials.");
            }


            return new AuthResult
            {
                Email = user.Email!,
                AccessToken = jwtTokenGenerator.CreateAccessToken(user),
                RefreshToken = jwtTokenGenerator.CreateRefreshToken()

            };
        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTime.UtcNow);

            if(user == null)
            {
                return null;
            }

            var newAccesToken = jwtTokenGenerator.CreateAccessToken(user);
            var newRefreshToken = jwtTokenGenerator.CreateRefreshToken();

            user.RefreshToken = newAccesToken;
            //user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(double.Parse(configuration["JwtSettings:ExpiresRefreshTokenInDays"]!));
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(2);

            await userManager.UpdateAsync(user);

            return new AuthResult
            {
                AccessToken = newAccesToken,
                RefreshToken = newRefreshToken
            };

        }
    }
}
