using Backend.Modules.Telegram.Interfaces;
using Backend.Modules.Telegram.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TelegramBotService;

namespace Backend.Modules.Telegram.Services;

public class TelegramLinkService : ITelegramLinkService
{
    private readonly ITelegramLinkTokenFactory _telegramLinkTokenFactory;
    private readonly ITelegramLinkTokenRepository _telegramLinkTokenRepository;
    private readonly TelegramSettings _telegramSettings;
    private readonly ITelegramUserRepository _telegramUserRepository;
    private readonly ITelegramUserFactory _telegramUserFactory;

    public TelegramLinkService(
        ITelegramLinkTokenRepository telegramLinkTokenRepository,
        ITelegramLinkTokenFactory telegramLinkTokenFactory,
        ITelegramUserRepository telegramUserRepository,
        ITelegramUserFactory telegramUserFactory,
        IOptions<TelegramSettings> telegramSettings)
    {
        _telegramLinkTokenRepository = telegramLinkTokenRepository;
        _telegramLinkTokenFactory = telegramLinkTokenFactory;
        _telegramUserRepository = telegramUserRepository;
        _telegramUserFactory = telegramUserFactory;
        _telegramSettings = telegramSettings.Value;

    }
    
    public async Task<string> GenerateLinkTokenAsync(Guid userId)
    {
        var token = _telegramLinkTokenFactory.Create(userId);
        await _telegramLinkTokenRepository.AddToken(token);

        var link = $"https://t.me/{_telegramSettings.BotUsername}?start={token.Token}";
        return link;
    }
    public async Task<TelegramLinkToken> VerifyTokenAsync(Guid tokenValue)
    {
        var token = await _telegramLinkTokenRepository.GetByToken(tokenValue);
        if (token == null) //TODO error
            return null;
        
        var expired = DateTime.UtcNow - token.CreatedAt > TimeSpan.FromMinutes(_telegramSettings.LinkTokenLifetimeMinutes);
        if (expired)
        {
            await _telegramLinkTokenRepository.DeleteToken(token);
            return null; //TODO error or api callback
        }

        return token;
    }

    public async Task<TelegramUser> LinkUser(Guid tokenValue, long chatId, string username)
    {
        var linkToken = await VerifyTokenAsync(tokenValue);
        if (linkToken == null)
            return null;
        
        var existTelegramUser = await _telegramUserRepository.GetByChatIdAsync(chatId);
        if (existTelegramUser != null) //TODO error or api callback
            return null;

        var telegramUser = _telegramUserFactory.Create(linkToken.UserId, chatId, username);
        await _telegramUserRepository.AddUserAsync(telegramUser);
        await _telegramLinkTokenRepository.DeleteToken(linkToken);
        
        return telegramUser;
    }

    public async Task UnLinkUser(TelegramUser user)
    {
        await _telegramUserRepository.DeleteUserAsync(user);
    }

    public async Task<TelegramUser> VerifyUserLink(Guid userId) =>
         await _telegramUserRepository.GetByUserIdAsync(userId);
}