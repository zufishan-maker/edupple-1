using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Interface
{
    public interface IEmailTemplate
    {
        string FromAddress { get; set; }

        string FromName { get; set; }

        string ReplyToAddress { get; set; }

        string ReplyToName { get; set; }

        string Subject { get; set; }

        string TextBody { get; set; }

        string HtmlBody { get; set; }
    }
}
