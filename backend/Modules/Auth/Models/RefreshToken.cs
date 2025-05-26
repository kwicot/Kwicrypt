using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Auth.Models;

[Serializable]
public class RefreshToken
{
    [Key]
    public Guid Id { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public RefreshToken() { }

    public RefreshToken(Guid id, Guid userId, DateTime expiresAt)
    {
        Id = id;
        Token = Token = Guid.NewGuid().ToString("N");
        UserId = userId;
        CreatedAt = DateTime.Now;
        ExpiresAt = expiresAt;
    }
}