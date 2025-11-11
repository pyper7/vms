using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using vms.application.Interfaces.Services;
using vms.shared.DTO;

namespace vms.infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly bool _useSsl;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "587");
            _smtpUsername = _configuration["EmailSettings:Username"] ?? string.Empty;
            _smtpPassword = _configuration["EmailSettings:Password"] ?? string.Empty;
            _fromEmail = _configuration["EmailSettings:FromEmail"] ?? string.Empty;
            _fromName = _configuration["EmailSettings:FromName"] ?? "VMS System";
            _useSsl = bool.Parse(_configuration["EmailSettings:UseSsl"] ?? "true");
        }

        public async Task SendEmailAsync(EmailMessageDto emailMessage)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_fromName, _fromEmail));
                message.To.Add(MailboxAddress.Parse(emailMessage.To));
                
                if (!string.IsNullOrEmpty(emailMessage.Cc))
                {
                    message.Cc.Add(MailboxAddress.Parse(emailMessage.Cc));
                }
                
                if (!string.IsNullOrEmpty(emailMessage.Bcc))
                {
                    message.Bcc.Add(MailboxAddress.Parse(emailMessage.Bcc));
                }

                message.Subject = emailMessage.Subject;

                var bodyBuilder = new BodyBuilder();
                if (emailMessage.IsHtml)
                {
                    bodyBuilder.HtmlBody = emailMessage.Body;
                }
                else
                {
                    bodyBuilder.TextBody = emailMessage.Body;
                }

                if (emailMessage.Attachments != null && emailMessage.Attachments.Any())
                {
                    foreach (var attachmentPath in emailMessage.Attachments)
                    {
                        if (File.Exists(attachmentPath))
                        {
                            bodyBuilder.Attachments.Add(attachmentPath);
                        }
                    }
                }

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, _useSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
                
                if (!string.IsNullOrEmpty(_smtpUsername) && !string.IsNullOrEmpty(_smtpPassword))
                {
                    await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation("Email sent successfully to {To}", emailMessage.To);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {To}", emailMessage.To);
                throw;
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            var emailMessage = new EmailMessageDto
            {
                To = to,
                Subject = subject,
                Body = body,
                IsHtml = isHtml
            };

            await SendEmailAsync(emailMessage);
        }

        public async Task SendBulkEmailAsync(List<string> recipients, string subject, string body, bool isHtml = true)
        {
            var tasks = recipients.Select(recipient => 
                SendEmailAsync(recipient, subject, body, isHtml)
            );

            await Task.WhenAll(tasks);
            _logger.LogInformation("Bulk email sent to {Count} recipients", recipients.Count);
        }
    }
}