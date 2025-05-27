using OtpNet;

namespace Kwicrypt.Module.Totp;

public static class TwoFactorHelper
{
    public static bool IsValidTotpSecret(string base32Secret, string userProvidedCode)
    {
        try
        {
            byte[] secretBytes = Base32Encoding.ToBytes(base32Secret);
            var totp = new OtpNet.Totp(secretBytes);
            
            return totp.VerifyTotp(userProvidedCode, out long _, new VerificationWindow(1, 1));
        }
        catch
        {
            return false;
        }
    }
    
    public static string GenerateTotpCode(string base32Secret)
    {
        try
        {
            byte[] secretBytes = Base32Encoding.ToBytes(base32Secret);
            
            var totp = new OtpNet.Totp(secretBytes);

            return totp.ComputeTotp();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при генерации кода: {ex.Message}");
            return string.Empty;
        }
    }
}