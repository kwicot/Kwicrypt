using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Auth.Persistent;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.Auth.Repositorys;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly RefreshTokenDbContext _context;

    public RefreshTokenRepository(RefreshTokenDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken token)
    {
        _context.List.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken> GetByTokenAsync(string token)
    {
        return await _context.List
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task RevokeAsync(RefreshToken token)
    {
        _context.List.Remove(token);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByUserIdAsync(Guid userId)
    {
        var tokens = _context.List.Where(x => x.UserId == userId);
        _context.List.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshToken token)
    {
        _context.List.Remove(token);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RefreshToken>> GetExpiredTokensAsync(int lifetimeMinutes)
    {
        var expirationTime = DateTime.UtcNow.AddMinutes(-lifetimeMinutes);
        return await _context.List
            .Where(t => t.CreatedAt < expirationTime)
            .ToListAsync();
    }

    public bool HasId(Guid id) =>
        _context.List.Any((token => token.Id == id));
}