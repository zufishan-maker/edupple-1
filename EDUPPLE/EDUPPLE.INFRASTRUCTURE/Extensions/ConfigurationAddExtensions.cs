using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Extensions
{
    public static class ConfigurationAddExtensions
    {
        public static void ConfigureAppServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (env == null) throw new ArgumentNullException(nameof(env));


            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient(); // this registers IHttpClientFactory, which we inject into some services to get HttpClients.

            services.AddScoped<ICurrentUser, CurrentUser>();



        }
    }
}
