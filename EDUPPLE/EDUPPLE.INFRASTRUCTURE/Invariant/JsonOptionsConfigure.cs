using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EDUPPLE.INFRASTRUCTURE.Invariant
{
    public class JsonOptionsConfigure
    {
        public static void ConfigureJsonOptions(MvcNewtonsoftJsonOptions jsonOptions)
        {
            jsonOptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}
