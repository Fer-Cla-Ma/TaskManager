using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string CreateAccessToken(ApplicationUser user);

    string CreateRefreshToken();
}
