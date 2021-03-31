using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.INFRASTRUCTURE.Options;
using EDUPPLE.INFRASTRUCTURE.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Provider
{
    public static class IdentityProvider
    {
        public static void AddIdentity(this IServiceCollection IdentityBuilder)
        {
            IdentityBuilder.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = false;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireUppercase = false;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Tokens.EmailConfirmationTokenProvider = "EmailConfirm";
            }).AddRoles<Role>()
             .AddEntityFrameworkStores<EduppleDbContext>()
             .AddTokenProvider<EmailConfirmationTokenProvider<User>>("EmailConfirm")
             .AddDefaultTokenProviders();
            IdentityBuilder.Configure<DataProtectionTokenProviderOptions>(opt =>
              opt.TokenLifespan = TimeSpan.FromHours(2));            
        }
        public static void AddIdentityAuthentication(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var principal = configuration.GetSection("Principal").Get<PrincipalConfiguration>();
            var hosting = configuration.GetSection("Hosting").Get<HostingConfiguration>();


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var key = Base64UrlTextEncoder.Decode(principal.AudienceSecret);
            var securityKey = new SymmetricSecurityKey(key);

            var validationParameters = new TokenValidationParameters
            {
                NameClaimType = TokenConstants.Claims.Name,
                RoleClaimType = TokenConstants.Claims.Role,
                ValidIssuer = hosting.ClientDomain,
                ValidAudience = principal.AudienceId,
                ValidateIssuer = hosting.ValidateIssuer,
                ValidateIssuerSigningKey = hosting.ValidateIssuer,
                IssuerSigningKey = securityKey,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = validationParameters;
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        },
                    };
                });
        }

    }
}
