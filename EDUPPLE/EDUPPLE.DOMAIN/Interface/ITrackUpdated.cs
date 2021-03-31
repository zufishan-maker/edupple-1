using System;
using System.Collections.Generic;
using System.Text;

namespace EDUPPLE.DOMAIN.Interface
{
    public interface ITrackUpdated
    {
        DateTimeOffset? UpdatedOn { get; set; }
        string UpdatedBy { get; set; }
    }
}
