using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EDUPPLE.DOMAIN.Entities
{
    public class User : IdentityUser<string>, ISoftDelete, ITrackCreated, ITrackUpdated, IHaveCode
    {
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public int OTP { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<RefreshToken> Tokens { get; set; } = new HashSet<RefreshToken>();
        public ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
    }
}
