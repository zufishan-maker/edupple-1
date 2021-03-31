using EDUPPLE.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Entities
{
    public class RefreshToken : Entity<long>, ITrackCreated, ITrackUpdated
    {      
        public string TokenHashed { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ProtectedTicket { get; set; }
        public DateTimeOffset Issued { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedOn { get;set; }
        public string CreatedBy { get;set; }
        public DateTimeOffset? UpdatedOn { get;set; }
        public string UpdatedBy { get;set; }
        public User User { get; set; }
    }
}
