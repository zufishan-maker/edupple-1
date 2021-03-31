using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.INFRASTRUCTURE.EmailService.Models;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.EmailTemplate.EmailService.Interface
{
    public interface IEmailDeliveryService
    {
        Task ProcessEmail(EmailDelivery emailDelivery, CancellationToken cancellationToken = default(CancellationToken));

        Task<SmtpResult> SendAsync(MimeMessage mimeMessage, CancellationToken cancellationToken);
    }
}
