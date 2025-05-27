using Kwicrypt.Module.Auth.Models;

namespace Kwicrypt.Module.Auth.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, RefreshToken refreshToken);
    Task<RefreshToken> GenerateRefreshTokenAsync(User user);
    Guid? ValidateAccessToken(string accessToken);
    Task<RefreshToken> ValidateRefreshTokenAsync(string refreshToken);
}