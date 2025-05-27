using Kwicrypt.Module.Telegram.Models;

namespace Kwicrypt.Module.Telegram.Interfaces;

public interface ITelegramLinkTokenFactory
{
    public TelegramLinkToken Create(Guid userId);
}