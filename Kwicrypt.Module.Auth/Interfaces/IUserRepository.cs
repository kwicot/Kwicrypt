using Kwicrypt.Module.Auth.Models;

namespace Kwicrypt.Module.Auth.Interfaces;

public interface IUserRepository
{
    public Task<User> FindUserByMail(string mail);
    public Task<User> FindUserById(Guid guid);
    
    public Task AddUser(User user);
    
    public bool HasMail(string mail);
    public bool HasId(Guid id);
    public Task UpdateUser(User user);
}