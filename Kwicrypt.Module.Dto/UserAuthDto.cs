using System;

namespace Kwicrypt.Module.Dto
{
    [Serializable]
    public class UserAuthDto
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string PublicRsaKey { get; set; }
    }
}