using Kwicrypt.Module.Cryptography.Interfaces;

namespace Kwicrypt.Module.Cryptography.Services;

public class CryptoService : ICryptoService
{
    private readonly string privateKey;
    private readonly string publicKey;
    
    
    public CryptoService()
    {
        var keys = CryptoHelper.CreateRsa();
        
        privateKey = keys.privateKey;
        publicKey = keys.publicKey;
    }

    public string GetPublicRsaKey() =>
        publicKey;

    public byte[] DecryptRsa(byte[] encryptedData) =>
        CryptoHelper.RsaDecrypt(encryptedData, privateKey);

    public byte[] EncryptRsa(byte[] data) =>
        CryptoHelper.RsaDecrypt(data, publicKey);
}