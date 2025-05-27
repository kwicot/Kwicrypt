using Kwicrypt.Module.LoginSecrets.Models;

namespace Kwicrypt.Module.LoginSecrets.Interfaces;

public interface ILoginDataRepository
{
    public Task<LoginSecret> GetLoginDataByIdAsync(Guid id);
    public Task<List<LoginSecret>> GetLoginDataListByUserIdAsync(Guid userId);
    
    public Task<LoginSecret> AddLoginDataAsync(LoginSecret loginSecret);
    
    public Task DeleteLoginDataByIdAsync(Guid id);
    public Task DeleteLoginDataByUserIdAsync(Guid userId);
    
    public Task UpdateLoginDataAsync(LoginSecret loginSecret);
}