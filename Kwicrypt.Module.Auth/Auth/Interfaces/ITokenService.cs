using Backend.Modules.Auth.Models;
using Backend.Modules.Data.Models;

namespace Backend.Modules.Auth.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, RefreshToken refreshToken);
    Task<RefreshToken> GenerateRefreshTokenAsync(User user);
    Guid? ValidateAccessToken(string accessToken);
    Task<RefreshToken> ValidateRefreshTokenAsync(string refreshToken);
}