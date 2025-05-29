using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;

namespace Kwicrypt.Module.Auth.Factorys;

public class UserFactory : IUserFactory
{
    private readonly IUserRepository _userRepository;

    public UserFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User GetUser(string mail, string password, string publicRsaKey)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var id = GetId();
        return new User(id, mail, passwordHash, publicRsaKey);
    }

    Guid GetId()
    {
        var id = Guid.NewGuid();
        if (_userRepository.HasId(id))
            return GetId();

        return id;
    }
}