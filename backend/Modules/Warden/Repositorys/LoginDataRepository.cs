using Backend.DBContext.Persistence;
using Backend.Modules.Warden.Interfaces;
using Backend.Modules.Warden.Models;

namespace Backend.Modules.Warden.Repositorys;

public class LoginDataRepository : ILoginDataRepository
{
    private readonly AppDbContext _context;

    public LoginDataRepository(
        AppDbContext context)
    {
        _context = context;
    }
    
    public Task<LoginData> GetLoginDataByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<LoginData>> GetLoginDataListByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<LoginData> AddLoginDataAsync(LoginData loginData)
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

    public Task UpdateLoginDataAsync(LoginData loginData)
    {
        throw new NotImplementedException();
    }
}