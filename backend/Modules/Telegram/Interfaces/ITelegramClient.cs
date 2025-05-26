using Backend.Modules.Telegram.Enums;

namespace Backend.Modules.Telegram.Interfaces;

public interface ITelegramClient
{
    void SendMessage(string message);
    void SendNotification(NotificationType type, string message);
}