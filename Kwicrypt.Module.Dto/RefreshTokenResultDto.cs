using System;

namespace Kwicrypt.Module.Dto
{
    [Serializable]
    public class RefreshTokenResultDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}