using Kwicrypt.Module.Email.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Kwicrypt.Module.Email.Services;

public class EmailService : IEmailService
{
    private readonly string _apiKey;

    public EmailService()
    {
        _apiKey = "";
    }
    
    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("Kwycript@example.com", "Password Vault");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
        await client.SendEmailAsync(msg);
    }
}