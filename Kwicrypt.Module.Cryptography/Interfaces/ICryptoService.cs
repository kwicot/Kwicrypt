using System.Net;
using Kwicrypt.Module.Cryptography.Models;

namespace Kwicrypt.Module.Cryptography.Interfaces;

public interface ICryptoService
{
    public string GetPublicRsaKey();
    
    public EncryptionResult DecryptRsa(byte[] encryptedData);
    public EncryptionResult<T> DecryptRsa<T>(byte[] encryptedData);
    
    public EncryptionResult EncryptRsa(byte[] data, string receiverPublicRsaKey);
    
}