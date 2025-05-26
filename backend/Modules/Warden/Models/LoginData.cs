using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Warden.Models;

public class LoginData
{
    [Key]
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    
    public string Name { get; private set; }
    public string Directory { get; private set; }
    public string SiteName { get; private set; }
    
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }
    public string TwoFactorSecret { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public LoginData(Guid id, Guid userId, string name, string login, string passwordHash)
    {
        Id = id;
        UserId = userId;
        
        Name = name;
        Directory = string.Empty;
        SiteName = string.Empty;
        
        Login = login;
        PasswordHash = passwordHash;
        TwoFactorSecret = string.Empty;
        
        CreatedAt = DateTime.Now;
        OnUpdate();
    }

    public void ChangeDirectory(string directory)
    {
        Directory = directory;
        OnUpdate();
    }

    public void ChangeTwoFactorCode(string code)
    {
        TwoFactorSecret = code;
        OnUpdate();
    }

    public void ChangeSiteName(string siteName)
    {
        SiteName = siteName;
        OnUpdate();
    }

    public void ChangeName(string name)
    {
        Name = name;
        OnUpdate();
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        OnUpdate();
    }

    public void ChangeLogin(string login)
    {
        Login = login;
        OnUpdate();
    }

    void OnUpdate()
    {
        UpdatedAt = DateTime.Now;
    }
    
}