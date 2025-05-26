using System.Text.RegularExpressions;
using Backend.Constants;

namespace Backend.Modules.Auth.Helpers;

public class UsernameValidator
{
    private static readonly Regex UsernameRegex = new Regex(
        @"^[a-zA-Z0-9]{6,}$",
        RegexOptions.Compiled);

    public static bool Validate(string username, out string errorCode)
    {
        errorCode = string.Empty;

        if (string.IsNullOrWhiteSpace(username))
        {
            errorCode = Errors.USERNAME_EMPTY;
            return false;
        }

        if (username.Length < 6)
        {
            errorCode = Errors.USERNAME_TOO_SHORT;
            return false;
        }

        if (!UsernameRegex.IsMatch(username))
        {
            errorCode = Errors.USERNAME_INVALID_CHARACTERS;
            return false;
        }

        return true;
    }
}