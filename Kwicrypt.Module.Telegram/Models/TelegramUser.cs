using Kwicrypt.Module.Core;

namespace Kwicrypt.Module.Telegram.Models;

[Serializable]
public class TelegramUser : DbModelBase
{
    public Guid UserId { get; private set; }
    public long ChatId { get; private set; }
    public string UserName { get; private set; }

    public TelegramUser(){}

    public TelegramUser(Guid id, Guid userId, long chatId, string userName) : base(id)
    {
        UserId = userId;
        ChatId = chatId;
        UserName = userName;
    }

}