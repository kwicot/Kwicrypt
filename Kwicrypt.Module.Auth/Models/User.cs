
using Kwicrypt.Module.Core;

namespace Kwicrypt.Module.Auth.Models;

public class User : DbModelBase
{
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    
    public User(){}
    
    public User(Guid id, string username, string passwordHash) : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
    }
}