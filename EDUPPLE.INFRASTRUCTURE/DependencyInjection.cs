using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.Options;
using EDUPPLE.INFRASTRUCTURE.Persistence;
using EDUPPLE.INFRASTRUCTURE.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE
{
    public static class DependencyInjection
    {       
       
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {               
            services.Configure<SmtpConfiguration>(_ => configuration.GetSection("SMTP").Bind(_));
            services.Configure<PrincipalConfiguration>(_ => configuration.GetSection("Principal").Bind(_));
            services.Configure<HostingConfiguration>(_ => configuration.GetSection("Hosting").Bind(_));
            services.AddDbContext<EduppleDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                   provider =>
                   {                     
                       provider.MigrationsAssembly(typeof(EduppleDbContext).Assembly.FullName);
                       provider.MigrationsHistoryTable(typeof(EduppleDbContext).Name, "dbo");
                       provider.CommandTimeout(1200);

                   }).UseSnakeCaseNamingConvention();                
            });           
           
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();     
            return services;
        }

     
       
    }
}
