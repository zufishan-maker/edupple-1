using EDUPPLE.APPLICATION.Authentication.User.Models;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Models;
using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using MimeKit;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.EmailTemplate.EmailService.Interface
{
    public interface IEmailTemplateService
    {
        Task<bool> SendResetPasswordEmail(UserResetPasswordEmail resetPassword);
        Task<MimeMessage> SendForgotPassword(UserForgotPasswordEmail forgotPasswordEmail);
        Task<bool> SendPassword(UserResetPasswordEmail resetPassword);
        Task<bool> SendUserInviteEmail(UserInviteEmail inviteEmail);
        Task<MimeMessage> SendTemplate<TModel>(IEmailTemplate emailTemplate, TModel emailModel)
            where TModel : EmailModelBase;
        Task<EmailDelivery> SendMessage(MimeMessage message);
        Task<IEmailTemplate> GetEmailTemplate(string templateKey);
        Task<IEmailTemplate> GetDatabaseTemplate(string templateKey);
        IEmailTemplate GetDatabaseTemplate(string templateKey, int companyId);
       
    }
}
