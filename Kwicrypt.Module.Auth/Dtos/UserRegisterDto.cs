using System.ComponentModel.DataAnnotations;

namespace Kwicrypt.Module.Auth.Dtos;

[Serializable]
public class UserRegisterDto
{
    [Required]
    public string Mail { get; set; }
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string PublicRsaKey { get; set; }

}