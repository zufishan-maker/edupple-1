using EDUPPLE.INFRASTRUCTURE.Model;
using Microsoft.AspNetCore.Http;
using UAParser;

namespace EDUPPLE.APPLICATION.Helper
{
    public static class HttpExtensions
    {
        [System.Obsolete]
        public static UserAgentModel UserAgent(this HttpRequest httpRequest)
        {
            var model = new UserAgentModel();

            if (httpRequest == null)
                return model;

            model.IpAddress = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
            model.UserAgent = httpRequest.Headers["User-Agent"].ToString();

            if (string.IsNullOrEmpty(model.UserAgent))
                return model;

            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(model.UserAgent);

            model.Browser = clientInfo.UserAgent.Family;
            model.DeviceBrand = clientInfo.Device.Brand;
            model.DeviceFamily = clientInfo.Device.Family;
            model.DeviceModel = clientInfo.Device.Model;
            model.OperatingSystem = clientInfo.OS.Family;

            return model;
        }
    }
}
