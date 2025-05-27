using System.ComponentModel.DataAnnotations;
using Kwicrypt.Module.Auth.Services;
using Kwicrypt.Module.Core.Constants;
using Kwicrypt.Module.Telegram.DTOs;
using Kwicrypt.Module.Telegram.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwicrypt.Module.Telegram.Controllers;

[Route("api/telegram")]
[ApiController]
public class TelegramController : ControllerBase
{
    private readonly UserAuthService _userAuthService;
    private readonly ITelegramService _telegramService;
    private readonly ITelegramLinkService _telegramLinkService;

    public TelegramController(
        UserAuthService userAuthService,
        ITelegramService telegramService,
        ITelegramLinkService telegramLinkService)
    {
        _userAuthService = userAuthService;
        _telegramService = telegramService;
        _telegramLinkService = telegramLinkService;
    }
    
    [HttpPost("generateLink")]
    [Authorize]
    public async Task<IActionResult> GenerateLink()
    {
        var userId = _userAuthService.TokenToUserId(Request);
        if (userId == null)
            return Unauthorized();

        var telegramUser = await _telegramLinkService.VerifyUserLink(userId);
        if (telegramUser != null)
            return BadRequest(Errors.TELEGRAM_USER_ALREADY_LINKED);

        var link = await _telegramLinkService.GenerateLinkTokenAsync(userId);

        return Ok(new { link });
    }
    
    [HttpPost("unlink")]
    [Authorize]
    public async Task<IActionResult> UnLink()
    {
        var userId = _userAuthService.TokenToUserId(Request);
        if (userId == null)
            return Unauthorized();

        var telegramUser = await _telegramLinkService.VerifyUserLink(userId);
        if (telegramUser == null)
            return BadRequest(Errors.TELEGRAM_USER_NOT_LINKED);

        await _telegramLinkService.UnLinkUser(telegramUser);

        return Ok();
    }

    [HttpPost("bot_callback")]
    public async Task<IActionResult> BotCallback([FromBody][Required] TelegramUserDTO userDto)
    {
        var telegramUser = await _telegramLinkService.LinkUser(userDto.Token, userDto.TelegramChatId, userDto.UserName);
        
        if(telegramUser == null)
            return BadRequest(Errors.TELEGRAM_TOKEN_NOT_FOUND);
        
        return Ok();
    }
    
}