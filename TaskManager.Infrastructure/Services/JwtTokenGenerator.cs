using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Settings;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _settings;
    public JwtTokenGenerator(IOptions<JwtSettings> options)
    {
        _settings = options.Value;
    }
    public string GenerateToken(User user)
    {
        var claims = new[]
       {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        // Se crea una clave de seguridad simétrica a partir de la clave secreta definida en la configuración JWT.
        // Esta clave se utiliza para firmar el token y garantizar su integridad.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        // Se crean las credenciales de firma utilizando la clave y el algoritmo HMAC-SHA256.
        // Estas credenciales se usan para firmar el JWT y asegurar que no ha sido modificado.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
