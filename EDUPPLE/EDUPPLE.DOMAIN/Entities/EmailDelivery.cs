using EDUPPLE.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Entities
{
    public class EmailDelivery : Entity<long>, ITrackCreated, ITrackUpdated
    {
        public bool IsProcessing { get; set; }
        public bool IsDelivered { get; set; }
        public DateTimeOffset? Delivered { get; set; }
        public DateTimeOffset? LastAttempt { get; set; }
        public DateTimeOffset? NextAttempt { get; set; }
        public string SmtpLog { get; set; }
        public string Error { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public Byte[] MimeMessage { get; set; }
        public int Attempts { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
