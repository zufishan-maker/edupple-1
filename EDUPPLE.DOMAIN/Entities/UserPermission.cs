using EDUPPLE.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Entities
{
    public class UserPermission: Entity<int>, ITrackCreated, ITrackUpdated, IHaveCode
    {
        public string Code { get; set; }
        public string UserId { get; set; }
        public int? PermissionId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}
