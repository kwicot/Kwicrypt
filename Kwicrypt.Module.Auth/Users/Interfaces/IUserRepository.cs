using Backend.Modules.Data.Models;

namespace Backend.Modules.Data.Interfaces;

public interface IUserRepository
{
    public Task<User> FindUser(string userName);
    public Task<User> FindUser(Guid guid);
    public Task AddUser(User user);
    public bool HasUsername(string userName);
    public bool HasId(Guid id);
}