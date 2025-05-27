using System.ComponentModel.DataAnnotations;

namespace Kwicrypt.Module.Auth.Dtos;

public class RefreshTokenRequestDto
{
    [Required]
    public string RefreshToken { get; set; }
}