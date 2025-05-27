using Backend.Modules.Telegram.Models;

namespace Backend.Modules.Telegram.Interfaces;

public interface ITelegramLinkService
{
    Task<string> GenerateLinkTokenAsync(Guid userId);
    Task<TelegramLinkToken> VerifyTokenAsync(Guid token);
    Task<TelegramUser> LinkUser(Guid tokenValue, long chatId, string username);
    Task UnLinkUser(TelegramUser user);
    Task<TelegramUser> VerifyUserLink(Guid userId);
}