using System.Security.Cryptography;
using Kwicrypt.Module.Cryptography.Models;

namespace Kwicrypt.Module.Cryptography.Interfaces
{

    public interface ICryptoService
    {
        public string GetPublicRsaKey();

        public EncryptionResult<T> DecryptRsa<T>(EncryptedData encryptedData);

        public EncryptionResult EncryptRsa(object data, string receiverPublicRsaKey);
    }
}