using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Identity;
using System;

namespace EDUPPLE.DOMAIN.Entities
{
    public class RoleClaim: IdentityRoleClaim<string>, ITrackCreated, ITrackUpdated
    {
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
