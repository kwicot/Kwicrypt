using System;

namespace Kwicrypt.Module.Dto
{
    [Serializable]
    public class ErrorDto
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}