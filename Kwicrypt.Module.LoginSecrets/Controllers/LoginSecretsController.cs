using System.ComponentModel.DataAnnotations;
using Kwicrypt.Module.Auth.Controllers;
using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Services;
using Kwicrypt.Module.LoginSecrets.DTOs;
using Kwicrypt.Module.LoginSecrets.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kwicrypt.Module.LoginSecrets.Controllers;

[Route("api/login-secrets")]
[ApiController]
public class LoginSecretsController : BaseController
{
    private readonly ILoginSecretsService _loginSecretsService;

    public LoginSecretsController(
        UserAuthService userAuthService,
        IUserRepository userRepository,
        ILoginSecretsService loginSecretsService) : base(userAuthService, userRepository)
    {
        _loginSecretsService = loginSecretsService;
    }

    [HttpPost("add-secret")]
    public async Task<IActionResult> AddSecret([FromBody][Required] LoginSecretDto loginSecretDto)
    {
        var authenticatedUser = await GetAuthenticatedUser();
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;
        
        await _loginSecretsService.AddSecret(authenticatedUser.User, loginSecretDto);
        return Ok();
    }

    [HttpGet("get-secrets")]
    public async Task<IActionResult> AddSecret()
    {
        var authenticatedUser = await GetAuthenticatedUser();
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;

        var secrets = await _loginSecretsService.GetSecretsDto(authenticatedUser.User);
        return Ok(secrets);
    }
    
    [HttpPost("remove-secrets")]
    public async Task<IActionResult> RemoveSecrets()
    {
        var authenticatedUser = await GetAuthenticatedUser();
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;

        await _loginSecretsService.DeleteSecretByUserId(authenticatedUser.User.Id);
        return Ok();
    }

    [HttpPost("update-secret")]
    public async Task<IActionResult> UpdateSecret([FromBody][Required] LoginSecretDto loginSecretDto)
    {
        var authenticatedUser = await GetAuthenticatedUser();
        if (authenticatedUser.ErrorResult != null) return authenticatedUser.ErrorResult;
        
        var success = await _loginSecretsService.UpdateSecretDto(loginSecretDto);
        if (success)
            return Ok();
        
        return BadRequest();
    }
    
    
}