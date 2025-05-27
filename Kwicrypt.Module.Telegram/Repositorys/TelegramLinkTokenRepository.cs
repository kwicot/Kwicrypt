using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;
using Kwicrypt.Module.Telegram.Persistent;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.Telegram.Repositorys;

public class TelegramLinkTokenRepository : ITelegramLinkTokenRepository
{
    private readonly TelegramLinkTokenDbContext _context;

    public TelegramLinkTokenRepository(TelegramLinkTokenDbContext context)
    {
        _context = context;
    }
    
    public async Task AddToken(TelegramLinkToken token)
    {
        _context.List.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteToken(TelegramLinkToken token)
    {
        _context.List.Remove(token);
        await _context.SaveChangesAsync();
    }

    public async Task<TelegramLinkToken> GetByToken(Guid token)
    {
        return await _context.List.FirstOrDefaultAsync((linkToken => linkToken.Token == token));
    }

    public async Task<List<TelegramLinkToken>> GetExpiredTokensAsync(int lifetimeMinutes)
    {
        var expirationTime = DateTime.UtcNow.AddMinutes(-lifetimeMinutes);
        return await _context.List
            .Where(t => t.CreatedAt < expirationTime)
            .ToListAsync();
    }

    public bool HasId(Guid id)
        => _context.List.Any((token => token.Id == id));
}