using System.ComponentModel.DataAnnotations;

namespace Kwicrypt.Module.Auth.Dtos;

[Serializable]
public class UserRegisterRequestDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

}