namespace Kwicrypt.Module.Auth.Dtos;

[Serializable]
public class UserLoginDto
{
    public string Mail { get; set; }
    public string Password { get; set; }
    public string PublicRsaKey { get; set; }
}