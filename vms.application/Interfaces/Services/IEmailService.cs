using vms.shared.DTO;

namespace vms.application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessageDto emailMessage);
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);
        Task SendBulkEmailAsync(List<string> recipients, string subject, string body, bool isHtml = true);
    }
}