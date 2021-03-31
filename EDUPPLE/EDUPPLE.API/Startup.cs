using Application;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Interface;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Service;
using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.INFRASTRUCTURE;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using EDUPPLE.INFRASTRUCTURE.Invariant;
using EDUPPLE.INFRASTRUCTURE.Persistence;
using EDUPPLE.INFRASTRUCTURE.Provider;
using EDUPPLE.INFRASTRUCTURE.StartUp;
using EDUPPLE.INFRASTRUCTURE.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Prometheus;
using Serilog;
using System;

namespace EDUPPLE.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddGrpc();
            services.AddCors();
            services.AddSingleton(this.Configuration);
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.ConfigureAppServices(WebHostEnvironment);
            services.AddDbContext(Configuration);
            services.AddApplication();
            services.AddAppApiVersioning().AddSwagger(WebHostEnvironment, Configuration);
            services.AddControllers().AddNewtonsoftJson(JsonOptionsConfigure.ConfigureJsonOptions);
            services.AddIdentity();
            services.AddIdentityAuthentication();
            services.AddHealthChecks()
                    .ForwardToPrometheus();
            services.AddMonitoring();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseCors(x => x
               .SetIsOriginAllowed(origin => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
            app.ConfigureMiddlewareForEnvironments(env);

            app.UseHttpsRedirection();
            app.UseSwaggerInDevAndStaging(env);

            //testing
            app.UseMigrationsEndPoint();           
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseHttpsRedirection();           
            app.UseAuthorization();
            app.UseMonitoring();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks();
                endpoints.MapMetrics();
                endpoints.MapControllers();
                endpoints.MapGrpcService<HealthService>();
            });          
        }
    }
}
