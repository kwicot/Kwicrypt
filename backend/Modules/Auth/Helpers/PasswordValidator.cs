using Backend.Constants;

namespace Backend.Modules.Auth.Helpers;

public class PasswordValidator
{
    public static bool Validate(string password, out string errorCode)
    {
        errorCode = string.Empty;

        if (string.IsNullOrWhiteSpace(password))
        {
            errorCode = Errors.PASSWORD_EMPTY;
            return false;
        }

        if (password.Length < 6)
        {
            errorCode = Errors.PASSWORD_TOO_SHORT;
            return false;
        }

        if (!password.Any(char.IsUpper))
        {
            errorCode = Errors.PASSWORD_NO_UPPERCASE;
            return false;
        }

        if (!password.Any(char.IsDigit))
        {
            errorCode = Errors.PASSWORD_NO_DIGIT;
            return false;
        }

        if (password.Any(char.IsWhiteSpace))
        {
            errorCode = Errors.PASSWORD_WHITESPACE;
            return false;
        }

        return true;
    }
}