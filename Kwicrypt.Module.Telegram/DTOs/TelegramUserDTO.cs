
using System.ComponentModel.DataAnnotations;

namespace Kwicrypt.Module.Telegram.DTOs;

[Serializable]
public class TelegramUserDTO
{
    [Required]
    public long TelegramChatId { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public Guid Token { get; set; }
}