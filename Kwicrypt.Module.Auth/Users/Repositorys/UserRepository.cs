using Backend.DBContext.Persistence;
using Backend.Modules.Data.Interfaces;
using Backend.Modules.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Data.Repositorys;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(
        AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<User> FindUser(string userName) => 
        await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);

    public async Task<User> FindUser(Guid guid) =>
        await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == guid);

    public async Task AddUser(User user)
    {
        _appDbContext.Users.Add(user);
        await _appDbContext.SaveChangesAsync();
    }

    public bool HasUsername(string userName) =>
        _appDbContext.Users.Any((user => user.Username == userName));

    public bool HasId(Guid id) =>
        _appDbContext.Users.Any((user => user.Id == id));
}