using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Entities
{
    public class UserLogin: IdentityUserLogin<string>, ITrackCreated, ITrackUpdated,IHaveIdentifier<int>
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
