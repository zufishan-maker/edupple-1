using EDUPPLE.INFRASTRUCTURE.Invariant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Swagger
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddAppApiVersioning(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(Invariant.StartUp.MajorVersion1, Invariant.StartUp.MinorVersion0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (hostingEnvironment == null) throw new ArgumentNullException(nameof(hostingEnvironment));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            //if (hostingEnvironment.IsDevelopmentOrIsStaging())
            //{
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Swagger.DocVersions.v1_0, CreateInfoForApiVersion(configuration, Swagger.DocVersions.v1_0));
                c.OperationFilter<RemoveVersionFromParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = apiDesc.CustomAttributes()
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == docName)
                           && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == docName));
                });


                c.AddSecurityDefinition(Identity.Bearer, new OpenApiSecurityScheme
                {
                    Description = Swagger.SecurityDefinition.Description,
                    In = ParameterLocation.Header,
                    Name = Swagger.SecurityDefinition.Name,
                    Type = SecuritySchemeType.Http
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id =Identity.Bearer
                                },
                                Scheme = Identity.Oauth2,
                                Name = Identity.Bearer,
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    });

                c.OperationFilter<SwaggerDefaultValuesFilter>();
                c.DescribeAllParametersInCamelCase();

            });

            return services;
        }

        public static void UseSwaggerInDevAndStaging(this IApplicationBuilder app, IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopmentOrIsStaging())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        $"/swagger/{Swagger.DocVersions.v1_0}/swagger.json",
                        $"edupple {Swagger.DocVersions.v1_0}"
                        );

                });
            }

        }


        private static OpenApiInfo CreateInfoForApiVersion(IConfiguration configuration, string version)
        {
            var info = new OpenApiInfo
            {
                Version = version,
                Title = Swagger.Info.Title,
                Description = Swagger.Info.Description,
                Contact = new OpenApiContact
                {
                    Name = Swagger.Info.ContactName,
                    Email = Swagger.Info.ContactEmail
                }
            };

            return info;
        }
    }
}
