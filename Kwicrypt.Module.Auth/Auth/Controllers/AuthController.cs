using System.ComponentModel.DataAnnotations;
using Backend.Constants;
using Backend.Modules.Auth.Dtos;
using Backend.Modules.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Auth.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserAuthService _userAuthService;

    public AuthController(
        UserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody][Required] UserRegisterRequestDto userRegisterRequestDto)
    {
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
    
    [HttpPost("revokeRefreshTokens")]
    public async Task<IActionResult> ClearRefreshTokens([FromBody][Required] string refreshToken)
    {
        var result = await _userAuthService.RevokeUserTokensAsync(refreshToken);
        if (result)
            return Ok();

        return BadRequest(Errors.REFRESH_TOKEN_EXPIRED);
    }
}