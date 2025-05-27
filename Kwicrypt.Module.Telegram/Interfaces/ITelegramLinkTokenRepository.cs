using Kwicrypt.Module.Telegram.Models;

namespace Kwicrypt.Module.Telegram.Interfaces;

public interface ITelegramLinkTokenRepository
{
    public Task AddToken(TelegramLinkToken token);
    public Task DeleteToken(TelegramLinkToken token);
    public Task<TelegramLinkToken> GetByToken(Guid token);
    public Task<List<TelegramLinkToken>> GetExpiredTokensAsync(int lifetimeMinutes);
    public bool HasId(Guid id);
}