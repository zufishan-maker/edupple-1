using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Options
{
    public class HostingConfiguration
    {
        public string ClientDomain { get; set; }
        public string ServiceDomain { get; set; }
        public string ServiceEndpoint { get; set; }
        public string Reset { get; set; }
        public bool ValidateIssuer { get; set; }
        
    }
}
