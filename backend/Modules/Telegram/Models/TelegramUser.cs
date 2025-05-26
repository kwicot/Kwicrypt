using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Telegram.Models;

[Serializable]
public class TelegramUser
{
    [Key] public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public long ChatId { get; private set; }
    public string UserName { get; private set; }

    public TelegramUser(){}

    public TelegramUser(Guid id, Guid userId, long chatId, string userName)
    {
        Id = id;
        UserId = userId;
        ChatId = chatId;
        UserName = userName;
    }

}