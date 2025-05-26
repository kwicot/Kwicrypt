using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Auth.Dtos;

[Serializable]
public class UserRegisterRequestDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

}