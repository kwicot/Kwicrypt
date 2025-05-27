using Kwicrypt.Module.LoginSecrets.Models;

namespace Kwicrypt.Module.LoginSecrets.Interfaces;

public interface ILoginSecretFactory
{
    public Task<LoginSecret> GetLoginDataAsync(
        Guid userId,
        string name,
        string login,
        string passwordHash,
        string directory,
        string siteName,
        string twoFactorCode);
}