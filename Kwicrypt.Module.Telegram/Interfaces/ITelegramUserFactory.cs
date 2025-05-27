using Backend.Modules.Telegram.Models;
using TelegramBotService;

namespace Backend.Modules.Telegram.Interfaces;

public interface ITelegramUserFactory
{
    TelegramUser Create(Guid userId, long chatId, string username);
}