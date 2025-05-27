using Kwicrypt.Module.Telegram.Models;

namespace Kwicrypt.Module.Telegram.Interfaces;

public interface ITelegramUserFactory
{
    TelegramUser Create(Guid userId, long chatId, string username);
}