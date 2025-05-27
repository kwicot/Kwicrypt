using Kwicrypt.Module.Telegram.Enums;

namespace Kwicrypt.Module.Telegram.Interfaces;

public interface ITelegramClient
{
    void SendMessage(string message);
    void SendNotification(NotificationType type, string message);
}