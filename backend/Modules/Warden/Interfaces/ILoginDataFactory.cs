using Backend.Modules.Warden.Models;

namespace Backend.Modules.Warden.Interfaces;

public interface ILoginDataFactory
{
    public Task<LoginData> GetLoginDataAsync(
        Guid userId,
        string name,
        string login,
        string passwordHash,
        string directory,
        string siteName,
        string twoFactorCode);
}