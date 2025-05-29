using System;

namespace Kwicrypt.Module.Dto
{
    [Serializable]
    public class LoginSecretDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string Site { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string TotpSecret { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}