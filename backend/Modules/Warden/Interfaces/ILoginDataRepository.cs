using Backend.Modules.Warden.Models;

namespace Backend.Modules.Warden.Interfaces;

public interface ILoginDataRepository
{
    public Task<LoginData> GetLoginDataByIdAsync(Guid id);
    public Task<List<LoginData>> GetLoginDataListByUserIdAsync(Guid userId);
    
    public Task<LoginData> AddLoginDataAsync(LoginData loginData);
    
    public Task DeleteLoginDataByIdAsync(Guid id);
    public Task DeleteLoginDataByUserIdAsync(Guid userId);
    
    public Task UpdateLoginDataAsync(LoginData loginData);
}