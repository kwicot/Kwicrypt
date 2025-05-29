using System;

namespace Kwicrypt.Module.Dto
{
    [Serializable]
    public class TelegramUserDTO
    {
        public long TelegramChatId { get; set; }
        public string UserName { get; set; }
        public Guid Token { get; set; }
    }
}