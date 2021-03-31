using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Extensions
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        string UserName { get; }
    }
}
