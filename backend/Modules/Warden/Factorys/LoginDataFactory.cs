using Backend.DBContext.Persistence;
using Backend.Modules.Warden.Interfaces;
using Backend.Modules.Warden.Models;

namespace Backend.Modules.Warden.Factorys;

public class LoginDataFactory : ILoginDataFactory
{
    private readonly AppDbContext _context;
    
    public LoginDataFactory(
        AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    
    public async Task<LoginData> GetLoginDataAsync(
        Guid userId,
        string name,
        string login,
        string passwordHash,
        string directory,
        string siteName,
        string twoFactorCode)
    {
        var id = await _context.GetId();

        var loginData = new LoginData(id, userId, name, login, passwordHash);
        loginData.ChangeDirectory(directory);
        loginData.ChangeSiteName(siteName);
        loginData.ChangeTwoFactorCode(twoFactorCode);

        return loginData;
    }
}