using System;

namespace Kwicrypt.Module.Dto
{
    [Serializable]
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
    }
}