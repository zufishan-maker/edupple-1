using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EDUPPLE.INFRASTRUCTURE.Invariant
{
    public static class WebHostEnvironmentExtensions
    {
        public static bool IsDevelopmentOrIsStaging(this IWebHostEnvironment webHostEnvironment)
        {
            return webHostEnvironment.IsDevelopment() || webHostEnvironment.IsStaging() || webHostEnvironment.IsProduction();
        }
    }
}
