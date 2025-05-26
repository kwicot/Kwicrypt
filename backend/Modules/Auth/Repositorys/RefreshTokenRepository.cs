using Backend.DBContext.Persistence;
using Backend.Modules.Auth.Models;
using Backend.Token;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Auth.Repositorys;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task RevokeAsync(RefreshToken token)
    {
        _context.RefreshTokens.Remove(token);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByUserIdAsync(Guid userId)
    {
        var tokens = _context.RefreshTokens.Where(x => x.UserId == userId);
        _context.RefreshTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshToken token)
    {
        _context.RefreshTokens.Remove(token);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RefreshToken>> GetExpiredTokensAsync(int lifetimeMinutes)
    {
        var expirationTime = DateTime.UtcNow.AddMinutes(-lifetimeMinutes);
        return await _context.RefreshTokens
            .Where(t => t.CreatedAt < expirationTime)
            .ToListAsync();
    }

    public bool HasId(Guid id) =>
        _context.RefreshTokens.Any((token => token.Id == id));
}