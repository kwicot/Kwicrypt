using System;
using Kwicrypt.Module.Cryptography.Constants;

namespace Kwicrypt.Module.Cryptography.Models
{
    public class EncryptionResult
    {
        public bool Success { get; private set; }
        public EncryptedData Result { get; private set; }
        public string Error { get; private set; }

        public EncryptionResult(EncryptedData result)
        {
            Success = true;
            Result = result;
            Error = Errors.NONE;
        }

        public EncryptionResult(string error)
        {
            Success = false;
            Result = default;
            Error = error;
        }
    }

    public class EncryptionResult<T>
    {
        public bool Success { get; private set; }
        public T Result { get; private set; }
        public string Error { get; private set; }

        public EncryptionResult(T result)
        {
            Success = true;
            Result = result;
            Error = Errors.NONE;
        }

        public EncryptionResult(string error)
        {
            Success = false;
            Result = default(T);
            Error = error;
        }
    }
}