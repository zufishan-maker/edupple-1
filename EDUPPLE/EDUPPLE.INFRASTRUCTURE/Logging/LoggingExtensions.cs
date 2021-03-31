using Microsoft.Extensions.Hosting;
using Serilog;

namespace EDUPPLE.INFRASTRUCTURE.Logging
{
    public static class LoggingExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration);
            });

            return hostBuilder;
        }
    }
}
