using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Interface;
using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.EmailService.Models;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using EDUPPLE.INFRASTRUCTURE.Options;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.EmailTemplate.EmailService.Service
{
    public class EmailDeliveryService : IEmailDeliveryService
    {
       
        private readonly IOptions<SmtpConfiguration> _smtpOptions;
        private readonly ILogger<EmailDeliveryService> _logger;

        public EmailDeliveryService(
            IOptions<SmtpConfiguration> smtpOptions,            
            ILogger<EmailDeliveryService> logger)
        {
            _smtpOptions = smtpOptions;          
            _logger = logger;
        }

        [Obsolete]
        public async Task<SmtpResult> SendAsync(MimeMessage mimeMessage, CancellationToken cancellationToken)
        {
            var result = new SmtpResult();

            var settings = _smtpOptions.Value;          

            var host = settings.Host;
            var port = settings.Port;
            var useSsl = settings.UseSSL;

            var userName = settings.UserName;
            var password = settings.Password;

            try
            {
                if (mimeMessage.From.Count == 0)
                    mimeMessage.From.Add(new MailboxAddress(settings.FromAddress)); 
               
                using var client = new SmtpClient();              
                await client.ConnectAsync(settings.Host, settings.Port, useSsl, default).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (userName.HasValue() || password.HasValue())
                    await client.AuthenticateAsync(userName, password, cancellationToken).ConfigureAwait(false);

                await client.SendAsync(mimeMessage, cancellationToken).ConfigureAwait(false);
                await client.DisconnectAsync(true, cancellationToken).ConfigureAwait(false);

                result.Successful = true;
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Exception = ex;
            }
            finally
            {               

                LogResult(mimeMessage, result, host);
            }

            return result;
        }


        public async Task ProcessEmail(EmailDelivery emailDelivery, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (emailDelivery == null)
                return;

            try
            {
                var toAddresses = emailDelivery.To;
                var subject = emailDelivery.Subject.Truncate(20);

                _logger.LogDebug("Processing email To: '{toAddresses}'; Subject: '{subject}'", toAddresses, subject);              

                var mimeMessage = await LoadMessage(emailDelivery, cancellationToken).ConfigureAwait(false);
                var smtpResult = await SendAsync(mimeMessage, cancellationToken).ConfigureAwait(false);               

                if (!smtpResult.Successful)
                {
                    throw smtpResult.Exception;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email: {message}", ex.Message);
                emailDelivery.Error = ex.ToString();
            }
        }

        private async Task<MimeMessage> LoadMessage(EmailDelivery emailDelivery, CancellationToken cancellationToken)
        {
            MimeMessage mimeMessage;

            using (var memoryStream = new MemoryStream(emailDelivery.MimeMessage, 0, emailDelivery.MimeMessage.Length))
            {
                memoryStream.Position = 0;
                mimeMessage = await MimeMessage.LoadAsync(memoryStream, cancellationToken).ConfigureAwait(false);
            }

            return mimeMessage;
        }

        private void LogResult(MimeMessage mimeMessage, SmtpResult result, string host)
        {
            var status = result.Successful ? "Sent" : "Error sending";
            var to = mimeMessage.To.ToString();
            var subject = mimeMessage.Subject.Truncate(20);
            var message = "{status} email to '{mimeMessage.To}' with subject '{subject}' using Host '{host}'";

            if (result.Successful)
                _logger.LogDebug(message, status, to, subject, host);
            else
                _logger.LogError(result.Exception, message, status, to, subject, host);

        }

        
    }
}
