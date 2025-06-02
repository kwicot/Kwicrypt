using System;
using System.Text;
using System.Threading.Tasks;
using Kwicrypt.Module.Cryptography.Interfaces;
using Kwicrypt.Module.Cryptography.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Core
{
    public class WebService : IWebService
    {
        private readonly ICryptoService _cryptoService;
        private readonly string _baseUrl;
        private readonly string _getRsaUrl = "/auth/rsa";

        private string _serverPublicRsaKey;
        
        
        public WebService(
            string baseUrl,
            ICryptoService cryptoService)
        {
            _baseUrl = baseUrl;
            _cryptoService = cryptoService;

            
            UpdateServerPublicRsa();
        }
        
        public async Task<WebRequestResult<T>> PostAsync<T>(string url, object data = null, string token = null) =>
            await SendRequestAsync<T>(url, "POST", data, token);

        public async Task<WebRequestResult<object>> PostAsync(string url, object data = null, string token = null) => 
            await SendRequestAsync<object>(url, "POST", data, token);

        public async Task<WebRequestResult<T>> GetAsync<T>(string url, object data = null, string token = null) =>
            await SendRequestAsync<T>(url, "GET", data, token);

        public async Task<WebRequestResult<object>> GetAsync(string url, object data = null, string token = null) => 
            await SendRequestAsync<object>(url, "GET", data, token);

        private async Task<WebRequestResult<T>> SendRequestAsync<T>(string url, string method, object data = null, string token = null)
        {
            try
            {
                var fullUrl = _baseUrl + url;
                Debug.Log($"Send [{method}] request to [{fullUrl}]");

                using var request = new UnityWebRequest(fullUrl, method)
                {
                    downloadHandler = new DownloadHandlerBuffer()
                };

                request.SetRequestHeader("Content-Type", "application/json");
                if (!string.IsNullOrEmpty(token))
                    request.SetRequestHeader("Authorization", $"Bearer {token}");

                if (data != null)
                {
                    var encryptionResult = await EncryptRequestDataAsync(data);
                    if (!encryptionResult.Success)
                        return Fail<T>(encryptionResult.Error);

                    request.uploadHandler = new UploadHandlerRaw(encryptionResult.Bytes);
                }

                var webOperation = request.SendWebRequest();
                while (!webOperation.isDone)
                    await Task.Yield();

                var responseCode = request.responseCode;
                var responseBody = request.downloadHandler.text;
                var isEncrypted = bool.TryParse(request.GetResponseHeader("X-Encrypted"), out var enc) && enc;
                var contentType = request.GetResponseHeader("Content-Type");

                Debug.Log($"Response code [{responseCode}] \n" +
                          $"Response data [{responseBody}]");

                if (responseCode != 200)
                    return Fail<T>(responseBody, (int)responseCode);

                if (isEncrypted)
                    return await HandleEncryptedResponse<T>(responseBody, responseCode);

                return HandlePlainResponse<T>(responseBody, contentType, responseCode);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
        
        private async Task<(bool Success, byte[] Bytes, string Error)> EncryptRequestDataAsync(object data)
        {
            if (string.IsNullOrEmpty(_serverPublicRsaKey))
                await UpdateServerPublicRsa();

            var encryptResult = _cryptoService.EncryptRsa(data, _serverPublicRsaKey);
            if (!encryptResult.Success)
                return (false, null, encryptResult.Error);

            var json = JsonConvert.SerializeObject(encryptResult.Result);
            var bytes = Encoding.UTF8.GetBytes(json);
            Debug.Log($"Data bytes {bytes.Length}");
            return (true, bytes, null);
        }

        private async Task<WebRequestResult<T>> HandleEncryptedResponse<T>(string responseBody, long code)
        {
            try
            {
                Debug.Log("Data encrypted");
                var encrypted = JsonConvert.DeserializeObject<EncryptedData>(responseBody);
                var decryptResult = _cryptoService.DecryptRsa<T>(encrypted);

                return decryptResult.Success
                    ? Success(decryptResult.Result, (int)code)
                    : Fail<T>(decryptResult.Error, (int)code);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return Fail<T>("Failed to decrypt or deserialize encrypted data.", (int)code);
            }
        }

        private WebRequestResult<T> HandlePlainResponse<T>(string body, string contentType, long code)
        {
            try
            {
                if (!string.IsNullOrEmpty(contentType) && contentType.Contains("text/plain"))
                {
                    return Success((T)(object)body, (int)code);
                }

                var result = JsonConvert.DeserializeObject<T>(body);
                return Success(result, (int)code);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return Fail<T>("Failed to deserialize response.", (int)code);
            }
        }

        private WebRequestResult<T> Success<T>(T result, int code = 200) => new() { Result = result, Code = code };

        private WebRequestResult<T> Fail<T>(string message, int code = 400) => new() { Message = message, Code = code };

        private async Task UpdateServerPublicRsa()
        {
            Debug.Log("Update server public rsa");
            var request = await SendRequestAsync<string>(_getRsaUrl, "GET");
            if (request.Success)
            {
                _serverPublicRsaKey = request.Result;
            }
            else
            {
                throw new NullReferenceException($"Something went wrong. [{request.Code}] [{request.Message}]");
            }
        }
    }
}