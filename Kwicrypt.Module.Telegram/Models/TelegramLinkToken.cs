using System.ComponentModel.DataAnnotations;
using Kwicrypt.Module.Core;

namespace Kwicrypt.Module.Telegram.Models;

[Serializable]
public class TelegramLinkToken : DbModelBase
{
    public Guid Token { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public TelegramLinkToken(){}

    public TelegramLinkToken(Guid id, Guid token, Guid userId) : base(id)
    {
        Token = token;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
}