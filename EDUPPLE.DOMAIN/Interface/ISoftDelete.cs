using System;
using System.Collections.Generic;
using System.Text;

namespace EDUPPLE.DOMAIN.Interface
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
