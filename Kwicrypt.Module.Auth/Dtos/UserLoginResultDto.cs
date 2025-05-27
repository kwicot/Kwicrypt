namespace Kwicrypt.Module.Auth.Dtos;

public class UserLoginResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}