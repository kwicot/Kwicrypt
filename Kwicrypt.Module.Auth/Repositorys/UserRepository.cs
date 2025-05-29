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
    
    public async Task<User> FindUserByMail(string mail) => 
        await _usersContext.List.FirstOrDefaultAsync(u => u.Mail == mail);

    public async Task<User> FindUserById(Guid guid) =>
        await _usersContext.List.FirstOrDefaultAsync(u => u.Id == guid);

    public async Task AddUser(User user)
    {
        _usersContext.List.Add(user);
        await _usersContext.SaveChangesAsync();
    }

    public bool HasMail(string mail) =>
        _usersContext.List.Any((user => user.Mail == mail));

    public bool HasId(Guid id) =>
        _usersContext.List.Any((user => user.Id == id));

    public async Task UpdateUser(User user)
    {
        await _usersContext.SaveChangesAsync();
    }
}