using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Telegram.Models;

[Serializable]
public class TelegramLinkToken
{
    [Key]
    public Guid Id { get; private set; }
    public Guid Token { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public TelegramLinkToken(){}

    public TelegramLinkToken(Guid id, Guid token, Guid userId)
    {
        Id = id;
        Token = token;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
}