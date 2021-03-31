using EDUPPLE.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Entities
{
    public class Permission: Entity<int>, ITrackCreated, ITrackUpdated, IHaveCode
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
     
    }
}
