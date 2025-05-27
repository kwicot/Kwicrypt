using Backend.Modules.Telegram.Models;

namespace Backend.Modules.Telegram.Interfaces;

public interface ITelegramLinkTokenFactory
{
    public TelegramLinkToken Create(Guid userId);
}