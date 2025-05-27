namespace Kwicrypt.Module.Auth.Models;

public class JwtSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationMinutes { get; set; }
    
    public int RefreshTokenCleanupIntervalMinutes { get; set; }
}