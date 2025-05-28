using Kwicrypt.Module.LoginSecrets.Models;

namespace Kwicrypt.Module.LoginSecrets.DTOs;

[Serializable]
public class LoginSecretDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Directory { get; set; }
    public string Site { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string TotpSecret { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }

    public LoginSecretDto() {}
    public LoginSecretDto(LoginSecret secret)
    {
        Id = secret.Id;
        Name = secret.Name;
        Directory = secret.Directory;
        Site = secret.SiteName;
        Login = secret.Login;
        Password = secret.PasswordHash;
        TotpSecret = secret.TotpSecret;
        
        CreatedAt = secret.CreatedAt.ToString("yyyy-mm-dd hh:mm:ss");
        UpdatedAt = secret.UpdatedAt.ToString("yyyy-mm-dd hh:mm:ss");
    }
}