using System.Net;
using System.Security.Cryptography;
using System.Text;
using Kwicrypt.Module.Core.Constants;
using Kwicrypt.Module.Cryptography.Interfaces;
using Kwicrypt.Module.Cryptography.Models;
using Newtonsoft.Json;

namespace Kwicrypt.Module.Cryptography.Services;

public class CryptoService : ICryptoService
{
    private readonly string privateRsaKey;
    private readonly string publicRsaKey;
    
    
    public CryptoService()
    {
        var keys = CryptoHelper.CreateRsa();
        
        privateRsaKey = keys.privateKey;
        publicRsaKey = keys.publicKey;
    }

    public string GetPublicRsaKey() =>
        publicRsaKey;

    public EncryptionResult DecryptRsa(byte[] encryptedData)
    {
        try
        {
            var decryptedData = CryptoHelper.DecryptRsa(encryptedData, privateRsaKey);
            return new EncryptionResult(decryptedData);
        }
        catch (ArgumentNullException ex)
        {
            return new EncryptionResult(Errors.MISSING_DATA);
        }
        catch (CryptographicException ex)
        {
            return new EncryptionResult(Errors.INVALID_DATA);
        }
    }

    public EncryptionResult<T> DecryptRsa<T>(byte[] encryptedData)
    {
        try
        {
            var encryptResult = DecryptRsa(encryptedData);
            if (encryptResult.Success)
            {
                var data = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(encryptResult.Result));
                if (data == null)
                    return new EncryptionResult<T>(Errors.MISSING_DATA);
                        
                return new EncryptionResult<T>(data);
            }
            
            return new EncryptionResult<T>(encryptResult.Error);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public EncryptionResult EncryptRsa(byte[] data, string receiverPublicRsaKey)
    {
        try
        {
            var encryptedData = CryptoHelper.EncryptRsa(data, receiverPublicRsaKey);
            return new EncryptionResult(encryptedData);
        }
        catch (ArgumentNullException ex)
        {
            return new EncryptionResult(Errors.MISSING_DATA);
        }
        catch (CryptographicException ex)
        {
            return new EncryptionResult(Errors.INVALID_DATA);
        }
    }
}