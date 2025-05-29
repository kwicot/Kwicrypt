using Kwicrypt.Module.Core.Constants;
using Kwicrypt.Module.Dto;

namespace Kwicrypt.Module.Auth.Models;

public class AuthResult
{
    public bool Success { get; }
    public string ErrorCode { get; }
    public string ErrorMessage { get; }
    public User User { get; }
    public string AccessToken { get; }
    public RefreshToken RefreshToken { get; }
    public ErrorDto ErrorDto => new ErrorDto() { Code = ErrorCode, Message = ErrorMessage };

    public AuthResult(User user)
    {
        Success = true;
        User = user;
        ErrorCode = string.Empty;
        ErrorMessage = string.Empty;
        
        AccessToken = string.Empty;
        RefreshToken = default;
    }

    public AuthResult(User user, string accessToken, RefreshToken refreshToken)
    {
        Success = true;
        User = user;
        ErrorCode = string.Empty;
        ErrorMessage = string.Empty;
        
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public AuthResult(string errorCode, string errorMessage)
    {
        Success = false;
        User = default;
        
        AccessToken = string.Empty;
        RefreshToken = default;
        
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public static AuthResult InvalidCredentialsResult =>
        new AuthResult(Errors.INVALID_CREDENTIALS, "Invalid credentials.");
}