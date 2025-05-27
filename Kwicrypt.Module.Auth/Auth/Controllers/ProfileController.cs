using Backend.Constants;
using Backend.Modules.Auth.Services;
using Backend.Modules.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Modules.Profile.Controllers;

[Route("api/profile")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly UserAuthService _userAuthService;

    public ProfileController(
        IUserRepository userRepository,
        UserAuthService userAuthService)
    {
        _userRepository = userRepository;
        _userAuthService = userAuthService;
    }
    
    [HttpGet("me")]
    [Authorize] // Требует авторизации
    public async Task<IActionResult> GetProfile()
    {
        var userId = _userAuthService.TokenToUserId(Request);
        if(userId == Guid.Empty)
            return Unauthorized(Errors.ACCESS_TOKEN_EXPIRED);

        var user = await _userRepository.FindUser(userId);
        if (user == null)
            return BadRequest(Errors.USERNAME_NOT_FOUND);

        return Ok(user);
    }

    

}