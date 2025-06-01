namespace Kwicrypt.Module.Email.Interfaces;

public interface IEmailService
{
    public Task SendEmailAsync(string toMail, string subject, string message);
}