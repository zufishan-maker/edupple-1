namespace EDUPPLE.APPLICATION.EmailTemplate.EmailService.Models
{
    public class UserResetPasswordEmail : EmailModelBase
    {
        public string ResetToken { get; set; }
        public int ExpireHours { get; set; } = 24;
    }
}
