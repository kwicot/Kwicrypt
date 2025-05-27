using Kwicrypt.Module.LoginSecrets.Interfaces;
using Kwicrypt.Module.LoginSecrets.Models;
using Kwicrypt.Module.LoginSecrets.Persistent;

namespace Kwicrypt.Module.LoginSecrets.Repositorys;

public class LoginDataRepository : ILoginDataRepository
{
    private readonly LoginSecretDbContext _contextBase;

    public LoginDataRepository(
        LoginSecretDbContext contextBase)
    {
        _contextBase = contextBase;
    }
    
    public Task<LoginSecret> GetLoginDataByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<LoginSecret>> GetLoginDataListByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<LoginSecret> AddLoginDataAsync(LoginSecret loginSecret)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLoginDataByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLoginDataByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLoginDataAsync(LoginSecret loginSecret)
    {
        throw new NotImplementedException();
    }
}