
using Kwicrypt.Module.Core;

namespace Kwicrypt.Module.Auth.Models;

[Serializable]
public class RefreshToken : DbModelBase
{
    public string Token { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public RefreshToken() { }

    public RefreshToken(Guid id, Guid userId, DateTime expiresAt) : base(id)
    {
        Token = Token = Guid.NewGuid().ToString("N");
        UserId = userId;
        CreatedAt = DateTime.Now;
        ExpiresAt = expiresAt;
    }
}