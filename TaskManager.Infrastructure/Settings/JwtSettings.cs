﻿namespace TaskManager.Infrastructure.Settings;

public class JwtSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiresAccessTokenInMinutes { get; set; }
    public int ExpiresRefreshTokenInDays { get; set; }
}
