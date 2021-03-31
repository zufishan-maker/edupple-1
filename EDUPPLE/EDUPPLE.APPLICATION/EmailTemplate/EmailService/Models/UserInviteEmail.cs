using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Models;

namespace EDUPPLE.APPLICATION.EmailTemplate.EmailService.Models
{
    public class UserInviteEmail : EmailModelBase
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
