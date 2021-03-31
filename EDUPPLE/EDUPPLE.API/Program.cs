using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EDUPPLE.INFRASTRUCTURE.Helper;
using EDUPPLE.INFRASTRUCTURE.Logging;
using EDUPPLE.INFRASTRUCTURE.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EDUPPLE.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
              
                CreateHostBuilder(args).Build()
                     .MigrateDbContext<EduppleDbContext>(true).Run();

            }
            catch (Exception ex)
            {                
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseLogging()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureAppConfiguration(config =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        config.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
                        config.AddEnvironmentVariables();
                        config.Build();

                    });                   
                });
    }
}
