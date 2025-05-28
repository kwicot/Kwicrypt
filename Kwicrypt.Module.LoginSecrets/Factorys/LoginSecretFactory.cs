using Kwicrypt.Module.LoginSecrets.Interfaces;
using Kwicrypt.Module.LoginSecrets.Models;
using Kwicrypt.Module.LoginSecrets.Persistent;

namespace Kwicrypt.Module.LoginSecrets.Factorys;

public class LoginSecretFactory : ILoginSecretFactory
{
    private readonly LoginSecretDbContext _contextBase;
    
    public LoginSecretFactory(
        LoginSecretDbContext dbContextBase)
    {
        _contextBase = dbContextBase;
    }
    
    public async Task<LoginSecret> GetLoginDataAsync(
        Guid userId,
        string name,
        string login,
        string passwordHash,
        string directory,
        string siteName,
        string twoFactorCode)
    {
        var id = await _contextBase.GetId();

        var loginData = new LoginSecret(id, userId, name, login, passwordHash);
        loginData.ChangeDirectory(directory);
        loginData.ChangeSiteName(siteName);
        loginData.ChangeTotpSecret(twoFactorCode);

        return loginData;
    }
}