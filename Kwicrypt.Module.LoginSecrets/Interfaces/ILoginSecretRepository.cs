using Kwicrypt.Module.LoginSecrets.Models;
using Kwicrypt.Module.LoginSecrets.Persistent;

namespace Kwicrypt.Module.LoginSecrets.Interfaces;

public interface ILoginSecretRepository
{
    public Task<LoginSecret> GetLoginDataByIdAsync(Guid id);
    public Task<List<LoginSecret>> GetLoginDataListByUserIdAsync(Guid userId);
    
    public Task<LoginSecret> AddLoginDataAsync(LoginSecret loginSecret);
    
    public Task DeleteLoginDataByIdAsync(Guid id);
    public Task DeleteLoginDataAsync(LoginSecret loginSecret);
    public Task DeleteLoginDataByUserIdAsync(Guid userId);
    
    public Task UpdateLoginDataAsync(LoginSecret loginSecret);
}