namespace ClinicOnline.Core.Interfaces;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string to, string subject, string name, string verificationLink);
}
