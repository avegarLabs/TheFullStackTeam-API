
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using TheFullStackTeam.Communications.EmailTemplates;
using TheFullStackTeam.Communications.EmailTemplates.Constants;
using TheFullStackTeam.Communications.EmailTemplates.Localization;
using TheFullStackTeam.Communications.EmailTemplates.ViewModels;
using TheFullStackTeam.Communications.Services.Abstract;
using TheFullStackTeam.Configuration;
using TheFullStackTeam.Extensions;

namespace TheFullStackTeam.Communications.Services
{
    /// <summary>
    /// Office 365 Mail Service
    /// </summary>
    /// <seealso cref="IEmailService" />
    public class Office365MailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IEmailViewRenderer _razorRender;
        private readonly ILogger<Office365MailService> _logger;
        private readonly LocalizationService _localizationService;
        private readonly MailAddress _senderMailAddress;


        /// <summary>Initializes a new instance of the <see cref="Office365MailService" /> class.</summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="razorRender">The razor render.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="localizationService">The localization service.</param>
        public Office365MailService(
            IConfiguration configuration,
            IEmailViewRenderer razorRender,
            ILogger<Office365MailService> logger,
            LocalizationService localizationService)
        {
            _emailSettings = configuration.GetSection<EmailSettings>();

            _razorRender = razorRender;
            _logger = logger;
            _localizationService = localizationService;
            _senderMailAddress = new MailAddress(_emailSettings.SmtpUsername,
                _localizationService.GetLocalizedHtmlString(EmailConstants.RES_SENDER_EMAIL_DISPLAYNAME));
        }

        /// <summary>Sends email asynchronous.</summary>
        /// <param name="subject">The subject.</param>
        /// <param name="htmlContent">Content of the HTML.</param>
        /// <param name="toAddressCollection">To address collection.</param>
        /// <param name="ccAddressCollection">The cc address collection.</param>
        public async Task SendEmailAsync(string subject, string htmlContent, MailAddressCollection toAddressCollection,
            MailAddressCollection ccAddressCollection = null)
        {
            var client = new SmtpClient()
            {
                Host = _emailSettings.SmtpAddress,
                Port = _emailSettings.SmtpPort,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername,_emailSettings.SmtpPassword),
                TargetName = $"STARTTLS/{_emailSettings.SmtpAddress}", // Set to avoid MustIssueStartTlsFirst exception
                EnableSsl = true,
            };

            var msg = new MailMessage()
            {
                From = _senderMailAddress,
            };

            foreach (var toMailAddress in toAddressCollection)
            {
                msg.To.Add(toMailAddress);
            }

            msg.Subject = subject ?? _localizationService.GetLocalizedHtmlString("DEFAULT_SUBJECT");
            msg.Body = htmlContent;
            msg.IsBodyHtml = true;

            if (ccAddressCollection != null)
            {
                foreach (var toMailAddress in ccAddressCollection)
                {
                    msg.Bcc.Add(toMailAddress);
                }
            }

            msg.ReplyToList.Add(_senderMailAddress);

            try
            {
                Console.WriteLine(msg.To + " - - " + msg.From);
                await client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        /// <summary>Sends emails with cc asynchronous.</summary>
        /// <param name="subject">The subject.</param>
        /// <param name="template">The template.</param>
        /// <param name="message">The message.</param>
        /// <param name="ccs">The CCS.</param>
        public async Task SendEmailsWithCcAsync(string subject, string template, string message,
            MailAddressCollection ccs)
        {
            try
            {
                if (ccs.Any())
                {
                    var toEmailAddress = ccs.FirstOrDefault();
                    ccs.Remove(toEmailAddress);

                    var htmlBody = await _razorRender.RenderViewAsync(template,
                        new VerifyAccountViewModel() { Message = message });
                    if (!string.IsNullOrEmpty(htmlBody))
                    {
                        await SendEmailAsync(subject, htmlBody, new MailAddressCollection { toEmailAddress }, ccs);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed sending email :: SendEmailsWithCCAsync");
            }
        }

        /// <summary>Sends the user registered admin email.</summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="ccs">The CCS.</param>
        public async Task SendUserRegisteredAdminEmail<TModel>(TModel model, MailAddressCollection ccs)
        {
            try
            {
                if (ccs.Any())
                {
                    var toEmailAddress = ccs.FirstOrDefault();
                    ccs.Remove(toEmailAddress);

                    var htmlBody =
                        await _razorRender.RenderViewAsync(TemplateNameConstants.NOTIFICATION_TO_ADMIN, model);
                    if (!string.IsNullOrEmpty(htmlBody))
                    {
                        await SendEmailAsync(
                            _localizationService.GetLocalizedHtmlString("USER_REGISTERED"),
                            htmlBody,
                            new MailAddressCollection { toEmailAddress },
                            ccs);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed sending email :: SendUserRegisteredAdminNotifications");
            }
        }

        /// <summary>Sends the professional welcome email.</summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="toEmailAddress">To email address.</param>
        public async Task SendProfessionalWelcomeEmail<TModel>(TModel model, string toEmailAddress)
        {
            try
            {
                var htmlBody =
                    await _razorRender.RenderViewAsync(TemplateNameConstants.PROFESSIONAL_CREATED_WELCOME_NOTIFICATION,
                        model);

                if (!string.IsNullOrEmpty(htmlBody))
                {
                    await SendEmailAsync(
                        _localizationService.GetLocalizedHtmlString(" PROFESSIONAL_WELCOME_EMAIL_TITLE"),
                        htmlBody,
                        new MailAddressCollection { toEmailAddress });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed sending email :: SendProfessionalWelcomeEmail");
            }
        }

        /// <summary>Sends the user welcome email.</summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="toEmailAddress">To email address.</param>
        /// <param name="ccs">The CCS.</param>
        public async Task SendUserWelcomeEmail<TModel>(TModel model, string toEmailAddress,
            MailAddressCollection ccs)
        {
            try
            {
                var htmlBody =
                    await _razorRender.RenderViewAsync(TemplateNameConstants.USER_WELCOME_NOTIFICATION, model);

                if (!string.IsNullOrEmpty(htmlBody))
                {
                    await SendEmailAsync(
                        _localizationService.GetLocalizedHtmlString("WELCOME_TO_TFST"),
                        htmlBody,
                        new MailAddressCollection { toEmailAddress },
                        ccs);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed sending email :: SendUserWelcomeNotifications");
            }
        }
    }
}