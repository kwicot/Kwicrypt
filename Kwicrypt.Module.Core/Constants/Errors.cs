namespace Kwicrypt.Module.Core.Constants;

public static class Errors
{
    public const string INVALID_DATA = "INVALID_DATA";
    public const string INVALID_CREDENTIALS = "INVALID_CREDENTIALS";

    public const string USERNAME_EXISTS = "USERNAME_EXISTS";
    public const string USERNAME_EMPTY = "USERNAME_EMPTY";
    public const string USERNAME_NOT_FOUND = "USERNAME_NOT_FOUND";
    public const string USERNAME_TOO_SHORT = "USERNAME_TOO_SHORT";
    public const string USERNAME_INVALID_CHARACTERS = "USERNAME_INVALID_CHARACTERS";
    
    public const string PASSWORD_EMPTY = "PASSWORD_EMPTY";
    public const string PASSWORD_TOO_SHORT = "PASSWORD_TOO_SHORT";
    public const string PASSWORD_NO_UPPERCASE = "PASSWORD_NO_UPPERCASE";
    public const string PASSWORD_NO_DIGIT = "PASSWORD_NO_DIGIT";
    public const string PASSWORD_WHITESPACE = "PASSWORD_WHITESPACE";
    
    public const string ACCESS_TOKEN_EXPIRED = "ACCESS_TOKEN_EXPIRED";
    public const string REFRESH_TOKEN_EXPIRED = "REFRESH_TOKEN_EXPIRED";
    
    public const string MARKET_UNSUPPORTED = "MARKET_UNSUPPORTED";
    
    public const string EXCHANGE_API_KEY_INVALID = "EXCHANGE_API_KEY_INVALID";
    
    public const string TELEGRAM_TOKEN_NOT_FOUND = "TELEGRAM_TOKEN_NOT_FOUND";
    public const string TELEGRAM_USER_ALREADY_LINKED = "TELEGRAM_USER_ALREADY_LINKED";
    public const string TELEGRAM_USER_NOT_LINKED = "TELEGRAM_USER_NOT_LINKED";
}