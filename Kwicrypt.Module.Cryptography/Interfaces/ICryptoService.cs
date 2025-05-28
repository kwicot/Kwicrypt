namespace Kwicrypt.Module.Cryptography.Interfaces;

public interface ICryptoService
{
    public string GetPublicRsaKey();
    public byte[] DecryptRsa(byte[] encryptedData);
    public byte[] EncryptRsa(byte[] data);
}