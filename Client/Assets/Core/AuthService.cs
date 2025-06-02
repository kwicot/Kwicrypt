using System.IO;
using System.Threading.Tasks;
using Kwicrypt.Module.Cryptography.Interfaces;
using Kwicrypt.Module.Dto;
using UnityEngine;

namespace Core
{
    public class AuthService : IAuthService
    {
        private string _accessToken;
        private string _refreshToken;
        private readonly ICryptoService _cryptoService;
        private readonly IWebService _webService;

        private string _registerUrl ="/auth/register";
        private string _loginUrl =  "/auth/login";
        private string _refreshTokenUrl = "/auth/refresh";
        private string _updateUserRsaUrl = "/auth/update-user-rsa";
        
        public AuthService(
            ICryptoService cryptoService,
            IWebService webService)
        {
            _cryptoService = cryptoService;
            _webService = webService;
        }

        public async Task<bool> RegisterUsingMailAsync(string mail, string password)
        {
            var userAuthDto = new UserAuthDto()
            {
                Mail = mail,
                Password = password,
                PublicRsaKey = _cryptoService.GetPublicRsaKey()
            };

            var requestOperation = await _webService.PostAsync(_registerUrl, userAuthDto);
            if (requestOperation.Success)
            {
                return true;
            }
            else
            {
                Debug.Log($"Error register request \n {requestOperation.Message}");
                return false;
            }
        }

        public async Task<bool> LoginUsingMailAsync(string mail, string password)
        {
            var userAuthDto = new UserAuthDto()
            {
                Mail = mail,
                Password = password,
                PublicRsaKey = _cryptoService.GetPublicRsaKey()
            };
            
            var requestOperation = await _webService.PostAsync<UserTokensDto>(_loginUrl, userAuthDto);
            if (requestOperation.Success)
            {
                var tokens = requestOperation.Result;
                _accessToken = tokens.AccessToken;
                _refreshToken = tokens.RefreshToken;
                
                return true;
            }
            else
            {
                Debug.Log($"Error login request \n {requestOperation.Message}");
                return false;
            }
        }

        public Task<bool> LogoutAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RevokeRefreshTokensAsync()
        {
            throw new System.NotImplementedException();
        }

        private Task<bool> RefreshAccessTokenAsync()
        {
            throw new System.NotImplementedException();
        }

    }
}