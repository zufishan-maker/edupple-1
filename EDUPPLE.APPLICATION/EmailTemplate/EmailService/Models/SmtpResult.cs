using System;

namespace EDUPPLE.INFRASTRUCTURE.EmailService.Models
{
    public class SmtpResult
    {
        public bool Successful { get; set; }

        public string SmtpLog { get; set; }

        public Exception Exception { get; set; }
    }
}
