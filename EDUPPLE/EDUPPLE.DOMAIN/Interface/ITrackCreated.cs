using System;
using System.Collections.Generic;
using System.Text;

namespace EDUPPLE.DOMAIN.Interface
{
    public interface ITrackCreated
    {
        DateTimeOffset CreatedOn { get; set; }
        string CreatedBy { get; set; }
    }
}
