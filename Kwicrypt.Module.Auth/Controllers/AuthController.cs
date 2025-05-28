using System.ComponentModel.DataAnnotations;
using Kwicrypt.Module.Auth.Dtos;
using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Services;
using Kwicrypt.Module.Core.Constants;

using Microsoft.AspNetCore.Mvc;

namespace Kwicrypt.Module.Auth.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : BaseController
{
    public AuthController(
        UserAuthService userAuthService,
        IUserRepository userRepository) : base(userAuthService, userRepository) {}


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody][Required] UserRegisterRequestDto userRegisterRequestDto)
    {
        
        var decryptedData = _cryptoService.DecryptRsa(registerData);
        
        
        if(!_userAuthService.ValidateRegisterCredentials(userRegisterRequestDto, out var errorCode))
            return BadRequest(errorCode);

        var result = await _userAuthService.RegisterAsync(userRegisterRequestDto);

        if (result.Success)
        {
            return Ok("Registration successful.");
        }

        return BadRequest(result.ErrorCode);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody][Required] UserLoginRequestDto userLoginRequestDto)
    {
        if(!_userAuthService.ValidateLoginCredentials(userLoginRequestDto, out var errorCode))
            return BadRequest(errorCode);

        var result = await _userAuthService.LoginAsync(userLoginRequestDto);

        if (result.Success)
        {
            return Ok(new UserLoginResultDto()
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken.Token
            });
        }

        return BadRequest(result.ErrorCode);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody][Required] string refreshToken)
    {
        var result = await _userAuthService.LogoutAsync(refreshToken);
        if (result)
            return Ok();

        return BadRequest(Errors.REFRESH_TOKEN_EXPIRED);
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody][Required] RefreshTokenRequestDto refreshRequest)
    {
        var result = await _userAuthService.RefreshTokensAsync(refreshRequest.RefreshToken);

        if (result.Success)
        {
            return Ok(new UserLoginResultDto()
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken.Token
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

    [HttpGet("me")]
    public async Task<IActionResult> GetProfile()
    {
        var authenticatedUser = await GetAuthenticatedUser();
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;

        return Ok(authenticatedUser.User);
    }
}