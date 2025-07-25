using Microsoft.AspNetCore.Identity;
using System.Data.Entity.Core.Objects;


namespace TaskManager.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public bool IsRevoked { get; set; } = false;
}
