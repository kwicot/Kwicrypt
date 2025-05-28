using Kwicrypt.Module.Core;

namespace Kwicrypt.Module.LoginSecrets.Models;

[Serializable]
public class LoginSecret : DbModelBase
{
    public Guid UserId { get; private set; }
    
    public string Name { get; private set; }
    public string Directory { get; private set; }
    public string SiteName { get; private set; }
    
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }
    public string TotpSecret { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public LoginSecret(){}

    public LoginSecret(Guid id, Guid userId, string name, string login, string passwordHash) : base(id)
    {
        UserId = userId;
        
        Name = name;
        Directory = string.Empty;
        SiteName = string.Empty;
        
        Login = login;
        PasswordHash = passwordHash;
        TotpSecret = string.Empty;
        
        CreatedAt = DateTime.Now;
        OnUpdate();
    }

    public void ChangeDirectory(string directory)
    {
        if(Directory == directory)
            return;
        
        Directory = directory;
        OnUpdate();
    }

    public void ChangeTotpSecret(string code)
    {
        if(TotpSecret == code)
            return;
        
        TotpSecret = code;
        OnUpdate();
    }

    public void ChangeSiteName(string siteName)
    {
        if(SiteName == siteName)
            return;
        
        SiteName = siteName;
        OnUpdate();
    }

    public void ChangeName(string name)
    {
        if(Name == name)
            return;
        
        Name = name;
        OnUpdate();
    }

    public void ChangePassword(string passwordHash)
    {
        if (PasswordHash == passwordHash)
            return;
        
        PasswordHash = passwordHash;
        OnUpdate();
    }

    public void ChangeLogin(string login)
    {
        if(Login == login)
            return;
        
        Login = login;
        OnUpdate();
    }

    void OnUpdate()
    {
        UpdatedAt = DateTime.Now;
    }
    
}