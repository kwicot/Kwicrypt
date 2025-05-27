using System.ComponentModel.DataAnnotations;

namespace Backend.Modules.Auth.Dtos;

public class RefreshTokenRequestDto
{
    [Required]
    public string RefreshToken { get; set; }
}