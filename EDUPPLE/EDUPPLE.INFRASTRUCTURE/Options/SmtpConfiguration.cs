using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Options
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSSL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FromAddress { get; set; }
        public string Logs { get; set; }
    }
}
