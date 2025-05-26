using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Data.Models;

public class User
{
    [Key]
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    
    public User(){}
    
    public User(Guid id, string username, string passwordHash)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
    }
}