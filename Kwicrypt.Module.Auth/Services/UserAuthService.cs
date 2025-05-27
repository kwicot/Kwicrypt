using Kwicrypt.Module.Auth.Dtos;
using Kwicrypt.Module.Auth.Helpers;
using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Core.Constants;

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
    
    public async Task<AuthResult> RegisterAsync(UserRegisterRequestDto requestDto)
    {
        var existingUser = await _userRepository.FindUser(requestDto.Username);
        
        if (existingUser != null)
        {
            return new AuthResult(Errors.USERNAME_EXISTS, "Username already exists");
        }

        var user = _userFactory.GetUser(requestDto.Username, requestDto.Password);

        await _userRepository.AddUser(user);

        return new AuthResult(user);
    }

    public async Task<AuthResult> LoginAsync(UserLoginRequestDto userLoginRequestDto)
    {
        var user = await _userRepository.FindUser(userLoginRequestDto.Username);
        if (user == null)
        {
            return AuthResult.InvalidCredentialsResult;
        }

        if (!BCrypt.Net.BCrypt.Verify(userLoginRequestDto.Password, user.PasswordHash))
            return AuthResult.InvalidCredentialsResult;

        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user, refreshToken);
        
        return new AuthResult(user, accessToken, refreshToken);
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

        var user = await _userRepository.FindUser(refreshToken.UserId);
        if (user == null)
        {
            return new AuthResult(Errors.USERNAME_NOT_FOUND, "User with refresh token not found");
        }

        await _refreshTokenRepository.RevokeAsync(refreshToken);

        var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user, newRefreshToken);

        return new AuthResult(user, accessToken, newRefreshToken);
    }

    public bool ValidateRegisterCredentials(UserRegisterRequestDto requestDto, out string errorCode)
    {
        if (!UsernameValidator.Validate(requestDto.Username, out errorCode))
            return false;

        if (!PasswordValidator.Validate(requestDto.Password, out errorCode))
            return false;

        return true;
    }
    public bool ValidateLoginCredentials(UserLoginRequestDto requestDto, out string errorCode)
    {
        if (!UsernameValidator.Validate(requestDto.Username, out errorCode))
            return false;

        if (!PasswordValidator.Validate(requestDto.Password, out errorCode))
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

    public async Task<bool> RevokeUserTokensAsync(string refreshToken)
    {
        var token = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (token == null)
            return false;

        var user = await _userRepository.FindUser(token.UserId);
        if (user == null)
            return false;

        await _refreshTokenRepository.DeleteByUserIdAsync(user.Id);
        return true;
    }
}