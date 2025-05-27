using Backend.DBContext.Persistence;
using Backend.Modules.Telegram.Interfaces;
using Backend.Modules.Telegram.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Telegram.Repositorys;

public class TelegramLinkTokenRepository : ITelegramLinkTokenRepository
{
    private readonly AppDbContext _context;

    public TelegramLinkTokenRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddToken(TelegramLinkToken token)
    {
        _context.TelegramLinkTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteToken(TelegramLinkToken token)
    {
        _context.TelegramLinkTokens.Remove(token);
        await _context.SaveChangesAsync();
    }

    public async Task<TelegramLinkToken> GetByToken(Guid token)
    {
        return await _context.TelegramLinkTokens.FirstOrDefaultAsync((linkToken => linkToken.Token == token));
    }

    public async Task<List<TelegramLinkToken>> GetExpiredTokensAsync(int lifetimeMinutes)
    {
        var expirationTime = DateTime.UtcNow.AddMinutes(-lifetimeMinutes);
        return await _context.TelegramLinkTokens
            .Where(t => t.CreatedAt < expirationTime)
            .ToListAsync();
    }

    public bool HasId(Guid id)
        => _context.TelegramLinkTokens.Any((token => token.Id == id));
}