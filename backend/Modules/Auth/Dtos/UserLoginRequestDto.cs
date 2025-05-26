using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Auth.Dtos;

[Serializable]
public class UserLoginRequestDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}