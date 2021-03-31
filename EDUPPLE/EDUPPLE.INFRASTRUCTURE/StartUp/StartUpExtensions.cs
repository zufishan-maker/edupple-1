using EDUPPLE.INFRASTRUCTURE.Options;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using Prometheus;
using System.Reflection;

namespace EDUPPLE.INFRASTRUCTURE.StartUp
{
    public static class StartUpExtensions
    {
        public static void MapHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }

        public static IServiceCollection AddMonitoring(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var tracingOptions = configuration.GetSection("OpenTelemetry:Tracing").Get<TracingOptions>();
            var serviceName = Assembly.GetCallingAssembly().GetName().Name;

            services.AddHealthChecks().ForwardToPrometheus();

            services.AddOpenTelemetryTracing(builder =>
            {
                builder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .SetSampler(new AlwaysOnSampler());                    
            });

            return services;
        }
        public static void UseMonitoring(this IApplicationBuilder app)
        {
            app.UseHttpMetrics();
            app.UseGrpcMetrics();
        }
    }
}
