using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EDUPPLE.INFRASTRUCTURE.Swagger
{
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var newPaths = new OpenApiPaths();

            foreach (var swaggerDocPath in swaggerDoc.Paths)
            {
                newPaths.Add(swaggerDocPath.Key.Replace("v{version}", swaggerDoc.Info.Version), swaggerDocPath.Value);
            }

            swaggerDoc.Paths = newPaths;
        }
    }
}
