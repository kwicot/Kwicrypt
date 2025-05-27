using Kwicrypt.Module.Auth.Models;

namespace Kwicrypt.Module.Auth.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken> GetByTokenAsync(string token);
    Task RevokeAsync(RefreshToken token);
    Task DeleteByUserIdAsync(Guid userId);
    Task DeleteAsync (RefreshToken token);
    Task<List<RefreshToken>> GetExpiredTokensAsync(int lifetimeMinutes);

    bool HasId(Guid id);
}