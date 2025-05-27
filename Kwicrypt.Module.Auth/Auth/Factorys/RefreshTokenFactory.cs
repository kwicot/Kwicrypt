using Backend.Modules.Auth.Interfaces;
using Backend.Modules.Auth.Models;
using Backend.Token;
using Microsoft.Extensions.Options;

namespace Backend.Modules.Auth.Factorys;

public class RefreshTokenFactory : IRefreshTokenFactory
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenFactory(
        IOptions<JwtSettings> jwtSettings,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public RefreshToken GetToken(Guid userId)
    {
        var id = GetId();
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpirationMinutes);

        var refreshToken = new RefreshToken(id, userId, expiresAt);
        return refreshToken;
    }

    Guid GetId()
    {
        var id = Guid.NewGuid();
        if (_refreshTokenRepository.HasId(id))
            return GetId();

        return id;
    }

}