using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;

namespace Kwicrypt.Module.Telegram.Factorys;

public class TelegramLinkTokenFactory : ITelegramLinkTokenFactory
{
    private readonly ITelegramLinkTokenRepository _telegramLinkTokenRepository;

    public TelegramLinkTokenFactory(ITelegramLinkTokenRepository telegramLinkTokenRepository)
    {
        _telegramLinkTokenRepository = telegramLinkTokenRepository;
    }
    
    public TelegramLinkToken Create(Guid userId)
    {
        var id = GetId();
        var token = Guid.NewGuid();
        return new TelegramLinkToken(id, token, userId);
    }
    
    Guid GetId()
    {
        var id = Guid.NewGuid();
        if (_telegramLinkTokenRepository.HasId(id))
            return GetId();

        return id;
    }
}