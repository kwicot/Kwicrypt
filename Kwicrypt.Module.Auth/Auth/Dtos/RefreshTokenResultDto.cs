namespace Backend.Modules.Auth.Dtos;

public class RefreshTokenResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}