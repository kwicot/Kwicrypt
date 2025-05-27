using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;
using Microsoft.Extensions.Options;

namespace Kwicrypt.Module.Telegram.Services;

public class TelegramService : ITelegramService
{
    private readonly ITelegramUserFactory _telegramUserFactory;
    private readonly ITelegramUserRepository _telegramUserRepository;
    
    private readonly ITelegramLinkService _telegramLinkService;
    
    private readonly TelegramSettings _telegramSettings;

    public TelegramService(
        ITelegramUserRepository telegramUserRepository,
        ITelegramUserFactory telegramUserFactory,
        ITelegramLinkService telegramLinkService,
        IOptions<TelegramSettings> telegramSettings)
    {
        _telegramUserRepository = telegramUserRepository;
        _telegramUserFactory = telegramUserFactory;
        _telegramLinkService = telegramLinkService;
        
        _telegramSettings = telegramSettings.Value;
    }
    
}