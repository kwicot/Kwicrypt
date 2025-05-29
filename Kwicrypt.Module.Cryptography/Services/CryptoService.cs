using System;
using System.Security.Cryptography;
using Kwicrypt.Module.Cryptography.Constants;
using Kwicrypt.Module.Cryptography.Interfaces;
using Kwicrypt.Module.Cryptography.Models;

namespace Kwicrypt.Module.Cryptography.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly string privateRsaKey;
        private readonly string publicRsaKey;
        private readonly RSA rsa;

        public CryptoService()
        {
            rsa = RSA.Create();
            rsa.KeySize = 2048;

            publicRsaKey = rsa.ToXmlString(false);
            privateRsaKey = rsa.ToXmlString(true);

            Console.WriteLine("Private RSA Key: " + privateRsaKey);
            Console.WriteLine($"Public RSA Key: {publicRsaKey}");
        }

        public string GetPublicRsaKey() => publicRsaKey;
        
        

        public EncryptionResult<T> DecryptRsa<T>(EncryptedData encryptedData)
        {
            try
            {
                Console.WriteLine($"Decrypting data with {privateRsaKey}");
                var encryptResult = CryptoHelper.DecryptObject<T>(encryptedData, privateRsaKey);
                return new EncryptionResult<T>(encryptResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public EncryptionResult EncryptRsa(object data, string receiverPublicRsaKey)
        {
            try
            {
                var encryptedData = CryptoHelper.EncryptObject(data, receiverPublicRsaKey);
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
}