using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Auth.Services;
using Kwicrypt.Module.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Kwicrypt.Module.Auth.Controllers;

public class BaseController : ControllerBase
{
    protected readonly UserAuthService _userAuthService;
    protected readonly IUserRepository _userRepository;

    protected BaseController(UserAuthService userAuthService, IUserRepository userRepository)
    {
        _userAuthService = userAuthService;
        _userRepository = userRepository;
    }

    protected async Task<AuthenticatedUser> GetAuthenticatedUser()
    {
        var userId = _userAuthService.TokenToUserId(Request);
        if (userId == Guid.Empty)
            return new AuthenticatedUser(null, Unauthorized(Errors.ACCESS_TOKEN_EXPIRED));

        var user = await _userRepository.FindUserById(userId);
        if (user == null)
            return new AuthenticatedUser(null, BadRequest(Errors.MAIL_NOT_FOUND));

        return new AuthenticatedUser(user, null);
    }
}