using Backend.Modules.Data.Interfaces;
using Backend.Modules.Data.Models;

namespace Backend.Modules.Data.Factorys;

public class UserFactory : IUserFactory
{
    private readonly IUserRepository _userRepository;

    public UserFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User GetUser(string username, string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var id = GetId();
        return new User(id, username, passwordHash);
    }

    Guid GetId()
    {
        var id = Guid.NewGuid();
        if (_userRepository.HasId(id))
            return GetId();

        return id;
    }
}