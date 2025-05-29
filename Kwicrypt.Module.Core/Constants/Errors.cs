namespace Kwicrypt.Module.Core.Constants;

public static class Errors
{
    public const string NONE = "NONE";
    public const string INVALID_DATA = "INVALID_DATA";
    public const string MISSING_DATA = "MISSING_DATA";
    public const string MISSING_OR_INVALID_DATA = "MISSING_OR_INVALID_DATA";
    public const string INVALID_CREDENTIALS = "INVALID_CREDENTIALS";

    public const string MAIL_EXISTS = "MAIL_EXISTS";
    public const string EMAIL_EMPTY = "EMAIL_EMPTY";
    public const string EMAIL_TOO_LONG = "EMAIL_TOO_LONG";
    public const string EMAIL_INVALID_FORMAT = "EMAIL_INVALID_FORMAT";
    public const string MAIL_NOT_FOUND = "USERNAME_NOT_FOUND";

    public const string USERNAME_EMPTY = "USERNAME_EMPTY";
    public const string USERNAME_TOO_SHORT = "USERNAME_TOO_SHORT";
    public const string USERNAME_INVALID_CHARACTERS = "USERNAME_INVALID_CHARACTERS";
    
    public const string PASSWORD_EMPTY = "PASSWORD_EMPTY";
    public const string PASSWORD_TOO_SHORT = "PASSWORD_TOO_SHORT";
    public const string PASSWORD_NO_UPPERCASE = "PASSWORD_NO_UPPERCASE";
    public const string PASSWORD_NO_DIGIT = "PASSWORD_NO_DIGIT";
    public const string PASSWORD_WHITESPACE = "PASSWORD_WHITESPACE";
    
    public const string ACCESS_TOKEN_EXPIRED = "ACCESS_TOKEN_EXPIRED";
    public const string REFRESH_TOKEN_EXPIRED = "REFRESH_TOKEN_EXPIRED";
    
    public const string TELEGRAM_TOKEN_NOT_FOUND = "TELEGRAM_TOKEN_NOT_FOUND";
    public const string TELEGRAM_USER_ALREADY_LINKED = "TELEGRAM_USER_ALREADY_LINKED";
    public const string TELEGRAM_USER_NOT_LINKED = "TELEGRAM_USER_NOT_LINKED";
}