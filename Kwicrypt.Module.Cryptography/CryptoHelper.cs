using System.Security.Cryptography;
using OtpNet;

namespace Kwicrypt.Module.Cryptography;


public static class CryptoHelper
{
    public static bool IsValidTotpSecret(string base32Secret, string userProvidedCode)
    {
        try
        {
            byte[] secretBytes = Base32Encoding.ToBytes(base32Secret);
            var totp = new OtpNet.Totp(secretBytes);
            
            return totp.VerifyTotp(userProvidedCode, out long _, new VerificationWindow(1, 1));
        }
        catch
        {
            return false;
        }
    }
    public static string GenerateTotpCode(string base32Secret)
    {
        Console.WriteLine($"Try generate totp code from {base32Secret}");
        if(string.IsNullOrEmpty(base32Secret))
            return string.Empty;
        
        try
        {
            byte[] secretBytes = Base32Encoding.ToBytes(base32Secret);
            
            var totp = new OtpNet.Totp(secretBytes);

            return totp.ComputeTotp();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при генерации кода: {ex.Message}");
            return string.Empty;
        }
    }
    public static Aes CreateAes(string base32Secret)
    {
        using var aes = Aes.Create();
        aes.GenerateKey();
        aes.GenerateIV();
        return aes;
    }
    public static byte[] AesEncrypt(byte[] data, Aes aes)
    {
        using var encryptor = aes.CreateEncryptor();
        return encryptor.TransformFinalBlock(data, 0, data.Length);
    }
    public static byte[] AesDecrypt(byte[] encryptedData, Aes aes)
    {
        using var decryptor = aes.CreateDecryptor();
        return decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
    }
    public static (string privateKey, string publicKey) CreateRsa(int keySize = 2048)
    {
        using var rsa = RSA.Create(keySize);
        var publicKey = rsa.ToXmlString(false);
        var privateKey = rsa.ToXmlString(true);
        return (privateKey, publicKey);
    }
    public static byte[] RsaEncrypt(byte[] data, string publicKeyXml)
    {
        using var rsa = RSA.Create();
        rsa.FromXmlString(publicKeyXml);
        return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
    }
    public static byte[] RsaDecrypt(byte[] encryptedData, string privateKeyXml)
    {
        using var rsa = RSA.Create();
        rsa.FromXmlString(privateKeyXml);
        return rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
    }
}