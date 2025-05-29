using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Kwicrypt.Module.Cryptography.Models;
using Newtonsoft.Json;

namespace Kwicrypt.Module.Cryptography
{
    public class CryptoHelper
    {
       public static EncryptedData EncryptObject<T>(T obj, string rsaPublicKeyXml)
        {
            string jsonData = JsonConvert.SerializeObject(obj);

            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                byte[] encryptedData;
                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(jsonData);
                    encryptedData = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }

                byte[] aesKeyIv = aes.Key.Concat(aes.IV).ToArray();
                byte[] encryptedAesKey;

                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(rsaPublicKeyXml);
                    encryptedAesKey = rsa.Encrypt(aesKeyIv, false);
                }

                var finalPackage = new EncryptedData()
                {
                    Key = Convert.ToBase64String(encryptedAesKey),
                    Data = Convert.ToBase64String(encryptedData)
                };

                return finalPackage;
            }
        }
        
        public static T DecryptObject<T>(EncryptedData encryptedString, string rsaPrivateKeyXml)
        {
            byte[] encryptedKey = Convert.FromBase64String(encryptedString.Key);
            byte[] encryptedData = Convert.FromBase64String(encryptedString.Data);

            byte[] aesKeyIv;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(rsaPrivateKeyXml);
                aesKeyIv = rsa.Decrypt(encryptedKey, false);
            }

            byte[] key = aesKeyIv.Take(32).ToArray(); // AES-256
            byte[] iv = aesKeyIv.Skip(32).ToArray();

            string json;
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    json = Encoding.UTF8.GetString(decryptedBytes);
                }
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }

}