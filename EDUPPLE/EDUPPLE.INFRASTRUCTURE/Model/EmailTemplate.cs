using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Model
{
    public class EmailTemplate
    {
        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public string ReplyToAddress { get; set; }

        public string ReplyToName { get; set; }

        public string Subject { get; set; }

        public string TextBody { get; set; }

        public string HtmlBody { get; set; }
    }
}
