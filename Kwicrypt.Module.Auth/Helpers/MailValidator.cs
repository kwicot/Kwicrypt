using System.Text.RegularExpressions;
using Kwicrypt.Module.Core.Constants;

namespace Kwicrypt.Module.Auth.Helpers;

public class MailValidator
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);


    public static bool Validate(string email, out string errorCode)
    {
        errorCode = string.Empty;

        if (string.IsNullOrWhiteSpace(email))
        {
            errorCode = Errors.EMAIL_EMPTY;
            return false;
        }

        if (email.Length > 320) // стандартный максимум
        {
            errorCode = Errors.EMAIL_TOO_LONG;
            return false;
        }

        if (!EmailRegex.IsMatch(email))
        {
            errorCode = Errors.EMAIL_INVALID_FORMAT;
            return false;
        }

        return true;
    }
}