using System.ComponentModel.DataAnnotations;
using System.Text;
using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Auth.Services;
using Kwicrypt.Module.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Kwicrypt.Module.Cryptography.Interfaces;
using Kwicrypt.Module.Cryptography.Models;
using Kwicrypt.Module.Dto;

namespace Kwicrypt.Module.Auth.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : BaseController
{
    private readonly ICryptoService _cryptoService;
    public AuthController(
        UserAuthService userAuthService,
        IUserRepository userRepository,
        ICryptoService cryptoService) : base(userAuthService, userRepository)
    {
        _cryptoService = cryptoService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody][Required] EncryptedData userRegisterDtoBytes)
    {
        var encryptionResult = _cryptoService.DecryptRsa<UserAuthDto>(userRegisterDtoBytes);
        if (!encryptionResult.Success)
            return BadRequest(encryptionResult.Error);

        var registerRequest = encryptionResult.Result;
        
        if(!_userAuthService.ValidateRegisterCredentials(registerRequest, out var errorCode))
            return BadRequest(errorCode);

        var result = await _userAuthService.RegisterAsync(registerRequest);

        if (result.Success)
        {
            return Ok("Registration successful.");
        }

        return BadRequest(result.ErrorCode);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody][Required] EncryptedData userLoginDtoJson)
    {
        var encryptionResult = _cryptoService.DecryptRsa<UserAuthDto>(userLoginDtoJson);
        if (!encryptionResult.Success)
            return BadRequest(encryptionResult.Error);
        
        var userLoginDto = encryptionResult.Result;
        
        if(!_userAuthService.ValidateLoginCredentials(userLoginDto, out var errorCode))
            return BadRequest(errorCode);

        var result = await _userAuthService.LoginAsync(userLoginDto);

        if (result.Success)
        {
            var userLoginResultDto = new UserTokensDto()
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken.Token,
            };
            
            var encryptOperation = _cryptoService.EncryptRsa(userLoginResultDto, userLoginDto.PublicRsaKey);
            if (!encryptOperation.Success)
                return BadRequest(encryptOperation.Error);
            
            return Ok(encryptOperation.Result);
        }

        return BadRequest(result.ErrorCode);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody][Required] EncryptedData refreshTokenJson)
    {
        var encryptionResult = _cryptoService.DecryptRsa<string>(refreshTokenJson);
        if (!encryptionResult.Success)
            return BadRequest(encryptionResult.Error);
        
        var refreshToken = encryptionResult.Result;
        
        var result = await _userAuthService.LogoutAsync(refreshToken);
        if (result)
            return Ok();

        return BadRequest(Errors.REFRESH_TOKEN_EXPIRED);
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody][Required] EncryptedData refreshTokenJson)
    {
        var decryptOperation = _cryptoService.DecryptRsa<string>(refreshTokenJson);
        if (!decryptOperation.Success)
            return BadRequest(decryptOperation.Error);
        
        string refreshToken = decryptOperation.Result;

        var authenticatedUser = await GetAuthenticatedUser(refreshToken);
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;
        
        User user = authenticatedUser.User;
        
        var result = await _userAuthService.RefreshTokensAsync(refreshToken);

        if (result.Success)
        {
            var userTokensDto = new UserTokensDto()
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken.Token,
            };

            var encryptOperation = _cryptoService.EncryptRsa(userTokensDto, user.PublicRsaKey);
            if (!encryptOperation.Success)
                return BadRequest(encryptOperation.Error);
            
            return Ok(encryptOperation.Result);
        }

        else
            return BadRequest(result.ErrorCode);
    }

    [HttpPost("update-user-rsa")]
    public async Task<IActionResult> UpdateUserRsa([FromBody] [Required] EncryptedData publicRsaEncrypted)
    {
        var authenticatedUser = await GetAuthenticatedUser();
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;

        var encryptOperation = _cryptoService.DecryptRsa<string>(publicRsaEncrypted);
        if (!encryptOperation.Success)
            return BadRequest(encryptOperation.Error);

        var user = authenticatedUser.User;
        
       await _userAuthService.UpdatePublicRsa(user, encryptOperation.Result);
       return Ok();
    }
    
    [HttpPost("revoke-refresh-tokens")]
    public async Task<IActionResult> ClearRefreshTokens([FromBody][Required] EncryptedData refreshTokenEncrypted)
    {
        var encryptOperation = _cryptoService.DecryptRsa<string>(refreshTokenEncrypted);
        if (!encryptOperation.Success)
            return BadRequest(encryptOperation.Error);
        
        var result = await _userAuthService.RevokeUserTokensAsync(encryptOperation.Result);
        if (result)
            return Ok();

        return BadRequest(Errors.REFRESH_TOKEN_EXPIRED);
    }
    [HttpGet("rsa")]
    public async Task<IActionResult> GetPublicRsaKey()
    {
        return Ok(_cryptoService.GetPublicRsaKey());
    }

    // [HttpGet("me")]
    // public async Task<IActionResult> GetProfile()
    // {
    //     var authenticatedUser = await GetAuthenticatedUser();
    //     if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;
    //     
    //     return Ok(authenticatedUser.User);
    // }
}