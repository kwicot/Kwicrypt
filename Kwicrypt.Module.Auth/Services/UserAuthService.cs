﻿using Kwicrypt.Module.Auth.Helpers;
using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Core.Constants;
using Kwicrypt.Module.Dto;

namespace Kwicrypt.Module.Auth.Services;

public class UserAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserFactory _userFactory;
    private readonly ITokenService _tokenService;
    
    public UserAuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUserFactory userFactory,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _userFactory = userFactory;
        _tokenService = tokenService;
    }
    
    public async Task<AuthResult> RegisterAsync(UserAuthDto dto)
    {
        var existingUser = await _userRepository.FindUserByMail(dto.Mail);
        
        if (existingUser != null)
        {
            return new AuthResult(Errors.MAIL_EXISTS, "Mail already registered");
        }

        var user = _userFactory.GetUser(dto.Mail, dto.Password, dto.PublicRsaKey);

        await _userRepository.AddUser(user);

        return new AuthResult(user);
    }

    public async Task<AuthResult> LoginAsync(UserAuthDto userAuthDto)
    {
        var user = await _userRepository.FindUserByMail(userAuthDto.Mail);
        if (user == null)
        {
            return AuthResult.InvalidCredentialsResult;
        }

        if (!BCrypt.Net.BCrypt.Verify(userAuthDto.Password, user.PasswordHash))
            return AuthResult.InvalidCredentialsResult;

        user.SetPublicRSAKey(userAuthDto.PublicRsaKey);
        await _userRepository.UpdateUser(user);
        
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user, refreshToken);
        
        return new AuthResult(user, accessToken, refreshToken);
    }

    public async Task UpdatePublicRsa(User user, string publicRsa)
    {
        user.SetPublicRSAKey(publicRsa);
        await _userRepository.UpdateUser(user);
    }
    public async Task<bool> LogoutAsync(string refreshToken)
    {
        var token = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (token == null)
            return false;

        await _refreshTokenRepository.RevokeAsync(token);
        return true;
    }
    
    public async Task<AuthResult> RefreshTokensAsync(string refreshTokenValue)
    {
        var refreshToken = await _tokenService.ValidateRefreshTokenAsync(refreshTokenValue);
        if (refreshToken == null)
        {
            return new AuthResult(Errors.REFRESH_TOKEN_EXPIRED, "Refresh token expired or invalid");
        }

        var user = await _userRepository.FindUserById(refreshToken.UserId);
        if (user == null)
        {
            return new AuthResult(Errors.MAIL_NOT_FOUND, "User with refresh token not found");
        }

        await _refreshTokenRepository.RevokeAsync(refreshToken);

        var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user, newRefreshToken);

        return new AuthResult(user, accessToken, newRefreshToken);
    }

    public bool ValidateRegisterCredentials(UserAuthDto dto, out string errorCode)
    {
        if (!MailValidator.Validate(dto.Mail, out errorCode))
            return false;

        if (!PasswordValidator.Validate(dto.Password, out errorCode))
            return false;

        return true;
    }
    public bool ValidateLoginCredentials(UserAuthDto dto, out string errorCode)
    {
        if (!MailValidator.Validate(dto.Mail, out errorCode))
            return false;

        if (!PasswordValidator.Validate(dto.Password, out errorCode))
            return false;

        return true;
    }
    public Guid TokenToUserId(HttpRequest request)
    {
        var accessToken = request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // Проверка валидности Access Token
        var userId = _tokenService.ValidateAccessToken(accessToken);

        if(userId.HasValue)
            return userId.Value;
        else
            return Guid.Empty;
    }
    public Guid RefreshTokenToUserId(string refreshToken)
    {
        // Проверка валидности Access Token
        var token = _tokenService.ValidateRefreshTokenAsync(refreshToken);

        if(token.Result != null)
            return token.Result.UserId;
        else
            return Guid.Empty;
    }

    public async Task<bool> RevokeUserTokensAsync(string refreshToken)
    {
        var token = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (token == null)
            return false;

        var user = await _userRepository.FindUserById(token.UserId);
        if (user == null)
            return false;

        await _refreshTokenRepository.DeleteByUserIdAsync(user.Id);
        return true;
    }
}