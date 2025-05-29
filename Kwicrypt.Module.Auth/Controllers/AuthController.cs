using System.ComponentModel.DataAnnotations;
using Kwicrypt.Module.Auth.Dtos;
using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Auth.Services;
using Kwicrypt.Module.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Kwicrypt.Module.Cryptography.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public async Task<IActionResult> Register([FromBody][Required] byte[] userRegisterDtoJson)
    {
        var encryptionResult = _cryptoService.DecryptRsa<UserRegisterDto>(userRegisterDtoJson);
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
    public async Task<IActionResult> Login([FromBody][Required] byte[] userLoginDtoJson)
    {
        var encryptionResult = _cryptoService.DecryptRsa<UserLoginDto>(userLoginDtoJson);
        if (!encryptionResult.Success)
            return BadRequest(encryptionResult.Error);
        
        var userLoginDto = encryptionResult.Result;
        
        if(!_userAuthService.ValidateLoginCredentials(userLoginDto, out var errorCode))
            return BadRequest(errorCode);

        var result = await _userAuthService.LoginAsync(userLoginDto);

        if (result.Success)
        {
            return Ok(new UserLoginResultDto()
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken.Token,
                PublicRsaKey = _cryptoService.GetPublicRsaKey()
            });
        }

        return BadRequest(result.ErrorCode);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody][Required] byte[] refreshTokenJson)
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
    public async Task<IActionResult> Refresh([FromBody][Required] byte[] refreshTokenJson)
    {
        var encryptionResult = _cryptoService.DecryptRsa<string>(refreshTokenJson);
        if (!encryptionResult.Success)
            return BadRequest(encryptionResult.Error);
        
        var refreshToken = encryptionResult.Result;
        
        var result = await _userAuthService.RefreshTokensAsync(refreshToken);

        if (result.Success)
        {
            return Ok(new UserLoginResultDto()
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken.Token,
                PublicRsaKey = _cryptoService.GetPublicRsaKey()
            });
        }

        else
            return BadRequest(result.ErrorCode);
    }
    
    [HttpPost("revoke-refresh-tokens")]
    public async Task<IActionResult> ClearRefreshTokens([FromBody][Required] string refreshToken)
    {
        var result = await _userAuthService.RevokeUserTokensAsync(refreshToken);
        if (result)
            return Ok();

        return BadRequest(Errors.REFRESH_TOKEN_EXPIRED);
    }
    [HttpGet("rsa")]
    public async Task<IActionResult> GetPublicRsaKey()
    {
        return Ok(_cryptoService.GetPublicRsaKey());
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetProfile()
    {
        var authenticatedUser = await GetAuthenticatedUser();
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;

        return Ok(authenticatedUser.User);
    }
}