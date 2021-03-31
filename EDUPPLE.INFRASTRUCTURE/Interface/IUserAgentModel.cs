using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Interface
{
    public interface IUserAgentModel
    {
        string UserAgent { get; set; }
        string Browser { get; set; }
        string OperatingSystem { get; set; }
        string DeviceFamily { get; set; }
        string DeviceBrand { get; set; }
        string DeviceModel { get; set; }
        string IpAddress { get; set; }
    }
}
