using Kwicrypt.Module.LoginSecrets.Interfaces;
using Kwicrypt.Module.LoginSecrets.Models;
using Kwicrypt.Module.LoginSecrets.Persistent;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.LoginSecrets.Repositorys;

public class LoginSecretRepository : ILoginSecretRepository
{
    private readonly LoginSecretDbContext _dbContext;

    public LoginSecretRepository(
        LoginSecretDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LoginSecret> GetLoginDataByIdAsync(Guid id) =>
        await _dbContext.List.FirstOrDefaultAsync((secret => secret.Id == id));

    public async Task<List<LoginSecret>> GetLoginDataListByUserIdAsync(Guid userId) =>
          await _dbContext.List.Where((secret => secret.UserId == userId)).ToListAsync();

    public async Task<LoginSecret> AddLoginDataAsync(LoginSecret loginSecret)
    {
        await _dbContext.List.AddAsync(loginSecret);
        await _dbContext.SaveChangesAsync();
        return loginSecret;
    }

    public async Task DeleteLoginDataByIdAsync(Guid id)
    {
        await DeleteLoginDataAsync(await GetLoginDataByIdAsync(id));
    }

    public async Task DeleteLoginDataAsync(LoginSecret loginSecret)
    {
        _dbContext.Remove(loginSecret);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteLoginDataByUserIdAsync(Guid userId)
    {
        var userLoginSecrets = await GetLoginDataListByUserIdAsync(userId);
        foreach (var loginSecret in userLoginSecrets.ToList())
        {
            await DeleteLoginDataAsync(loginSecret);
        }
    }

    public async Task UpdateLoginDataAsync(LoginSecret loginSecret)
    {
        var isModified = _dbContext.List.Entry(loginSecret).State == EntityState.Modified;
        if (isModified)
            await _dbContext.SaveChangesAsync();
    }
}