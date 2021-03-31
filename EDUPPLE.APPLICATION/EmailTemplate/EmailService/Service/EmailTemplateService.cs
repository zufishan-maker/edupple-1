using EDUPPLE.APPLICATION.Authentication.User.Models;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Interface;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Models;
using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.EmailTemplate.EmailService.Service
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IUnitOfWork _dataContext;       
        private readonly IEmailDeliveryService _emailDeliveryService;
        private ILogger Logger { get; set; }


        public EmailTemplateService(ILoggerFactory loggerFactory,
            IEmailDeliveryService emailDeliveryService,
            IServiceProvider provider)
        {
            _dataContext = (IUnitOfWork)provider.GetService(typeof(IUnitOfWork));
            _emailDeliveryService = emailDeliveryService;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        [Obsolete]
        public async Task<MimeMessage> SendForgotPassword(UserForgotPasswordEmail forgotPasswordEmail)
        {
            try
            {
                var emailTemplate = await GetEmailTemplate(Templates.ForgotPassword).ConfigureAwait(false);
              return  await SendTemplate(emailTemplate, forgotPasswordEmail).ConfigureAwait(false);
              
            }catch(Exception ex)
            {
                Logger.LogError("Get Email Template For Forgot Password:" + ex.Message);               
            }
            return default;
        }

        [Obsolete]
        public async Task<bool> SendPassword(UserResetPasswordEmail resetPassword)
        {
            var emailTemplate = await GetEmailTemplate(Templates.ChangePassword).ConfigureAwait(false);
            await SendTemplate(emailTemplate, resetPassword).ConfigureAwait(false);
            return true;
        }

        [Obsolete]
        public async Task<bool> SendResetPasswordEmail(UserResetPasswordEmail resetPassword)
        {
            var emailTemplate = await GetEmailTemplate(Templates.ResetPassword).ConfigureAwait(false);

            await SendTemplate(emailTemplate, resetPassword).ConfigureAwait(false);

            return true;
        }

        [Obsolete]
        public async Task<bool> SendUserInviteEmail(UserInviteEmail inviteEmail)
        {
            var emailTemplate = await GetEmailTemplate(Templates.UserInvite).ConfigureAwait(false);

            await SendTemplate(emailTemplate, inviteEmail).ConfigureAwait(false);

            return true;
        }       

        public async Task<MimeMessage> SendTemplate<TModel>(IEmailTemplate emailTemplate, TModel emailModel)
            where TModel : EmailModelBase
        {
            var subjectTemplate = Handlebars.Compile(emailTemplate.Subject);
            var htmlBodyTemplate = Handlebars.Compile(emailTemplate.HtmlBody);
            var textBodyTemplate = Handlebars.Compile(emailTemplate.TextBody);

            var subject = subjectTemplate(emailModel);
            var htmlBody = htmlBodyTemplate(emailModel);
            var textBody = textBodyTemplate(emailModel);

            var message = new MimeMessage();
            message.Headers.Add("X-Mailer-Machine", Environment.MachineName);
            message.Headers.Add("X-Mailer-Date", DateTime.Now.ToString(CultureInfo.InvariantCulture));

            message.Subject = subject;

            message.From.Add(new MailboxAddress(emailTemplate.FromName, emailTemplate.FromAddress));

            if (emailTemplate.ReplyToAddress.HasValue())
                message.ReplyTo.Add(new MailboxAddress(emailTemplate.ReplyToName, emailTemplate.ReplyToAddress));

            message.To.Add(new MailboxAddress(emailModel.DisplayName, emailModel.EmailAddress));

            var builder = new BodyBuilder
            {
                TextBody = textBody,
                HtmlBody = htmlBody                
            };

            message.Body = builder.ToMessageBody();
            return await Task.FromResult(message);
        }

        public async Task<EmailDelivery> SendMessage(MimeMessage message)
        {
            var emailDelivery = new EmailDelivery
            {
                From = message.From.ToDelimitedString(";").Truncate(256),
                To = message.To.ToDelimitedString(";").Truncate(256),
                Subject = message.Subject.Truncate(256)
            };
            try
            {            

                using (var memoryStream = new MemoryStream())
                {
                    await message.WriteToAsync(memoryStream).ConfigureAwait(false);
                    emailDelivery.MimeMessage = memoryStream.ToArray();
                }
                emailDelivery.NextAttempt = DateTimeOffset.UtcNow;              
                await _emailDeliveryService.ProcessEmail(emailDelivery, default).ConfigureAwait(false);             
                
            }
            catch(Exception ex)
            {
                
            }
            return await Task.FromResult(emailDelivery);
        }

        [Obsolete]
        public async Task<IEmailTemplate> GetEmailTemplate(string templateKey)
        {           
                var template = await GetDatabaseTemplate(templateKey).ConfigureAwait(false);               
                return template;
         
        }

        [Obsolete]
        public IEmailTemplate GetEmailTemplate(string templateKey, int companyId)
        {
            var template = GetDatabaseTemplate(templateKey, companyId);
            return template;

        }
        public async Task<IEmailTemplate> GetDatabaseTemplate(string templateKey)
        {
            var template = _dataContext.Set<DOMAIN.Entities.EmailTemplate>().AsNoTracking()
                        .Where(x => x.Key.Equals(templateKey)).FirstOrDefault();
            return await Task.FromResult(template).ConfigureAwait(false);

        }
        public IEmailTemplate GetDatabaseTemplate(string templateKey, int companyId)
        {
            var template = _dataContext.Set<DOMAIN.Entities.EmailTemplate>().AsNoTracking()
                       .Where(x => x.Key.Equals(templateKey)).FirstOrDefault();
            return template;
        }

      

        public static class Templates
        {
            public const string ResetPassword = "reset-password";
            public const string PasswordlessLogin = "passwordless-login";
            public const string UserInvite = "user-invite";
            public const string ChangePassword = "change-password";
            public const string ForgotPassword = "forgot-password";
        }
    }
}
