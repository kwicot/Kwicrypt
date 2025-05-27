using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Auth.Persistent;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.Auth.Repositorys;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _usersContext;

    public UserRepository(
        UserDbContext usersContext)
    {
        _usersContext = usersContext;
    }
    
    public async Task<User> FindUser(string userName) => 
        await _usersContext.List.FirstOrDefaultAsync(u => u.Username == userName);

    public async Task<User> FindUser(Guid guid) =>
        await _usersContext.List.FirstOrDefaultAsync(u => u.Id == guid);

    public async Task AddUser(User user)
    {
        _usersContext.List.Add(user);
        await _usersContext.SaveChangesAsync();
    }

    public bool HasUsername(string userName) =>
        _usersContext.List.Any((user => user.Username == userName));

    public bool HasId(Guid id) =>
        _usersContext.List.Any((user => user.Id == id));
}