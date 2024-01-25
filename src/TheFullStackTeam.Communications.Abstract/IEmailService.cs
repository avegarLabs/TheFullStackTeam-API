using System.Net.Mail;

namespace TheFullStackTeam.Communications.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string htmlContent, MailAddressCollection To, MailAddressCollection Ccs = null);
        Task SendEmailsWithCcAsync(string subject, string template, string message, MailAddressCollection ccs);
        Task SendUserWelcomeEmail<TModel>(TModel model, string toEmailAddress, MailAddressCollection ccs);
        Task SendUserRegisteredAdminEmail<TModel>(TModel model, MailAddressCollection ccs);
        Task SendProfessionalWelcomeEmail<TModel>(TModel model, string email);
    }
}
