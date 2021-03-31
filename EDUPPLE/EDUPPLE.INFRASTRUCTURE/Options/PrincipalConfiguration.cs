using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Options
{
    public class PrincipalConfiguration
    {
        public string AudienceId { get; set; }
        public string AudienceSecret { get; set; }
        public TimeSpan TokenExpire { get; set; } = TimeSpan.FromDays(1);
        public TimeSpan RefreshExpire { get; set; } = TimeSpan.FromDays(1);
    }
}
