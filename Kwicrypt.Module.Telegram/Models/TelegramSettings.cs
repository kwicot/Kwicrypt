namespace Backend.Modules.Telegram.Models;

public class TelegramSettings
{
    public string BotUsername { get; set; }
    public string BotToken { get; set; }
    public int LinkTokenLifetimeMinutes { get; set; }
    public int LinkTokenCleanupIntervalMinutes { get; set; }
}