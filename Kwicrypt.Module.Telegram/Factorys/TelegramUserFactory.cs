using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;

namespace Kwicrypt.Module.Telegram.Factorys;

public class TelegramUserFactory : ITelegramUserFactory
{
    private readonly ITelegramUserRepository _telegramUserRepository;

    public TelegramUserFactory(ITelegramUserRepository telegramUserRepository)
    {
        _telegramUserRepository = telegramUserRepository;
    }
    
    
    public TelegramUser Create(Guid userId, long chatId, string username)
    {
        var id = GetId();
        return new TelegramUser(id, userId, chatId, username);
    }

    Guid GetId()
    {
        var id = Guid.NewGuid();
        if (_telegramUserRepository.HasId(id))
            return GetId();

        return id;
    }
}