using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Models;

namespace EDUPPLE.APPLICATION.Authentication.User.Models
{
    public class UserForgotPasswordEmail: EmailModelBase
    {
        public string ResetToken { get; set; }      
        public int ExpireHours { get; set; } = 24;
    }
}
