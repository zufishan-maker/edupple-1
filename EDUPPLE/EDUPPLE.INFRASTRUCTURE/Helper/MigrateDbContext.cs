using EDUPPLE.INFRASTRUCTURE.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace EDUPPLE.INFRASTRUCTURE.Helper
{
    public static class Migrate
    {
        public static IHost MigrateDbContext<TContext>(
          this IHost host, bool drop)
          where TContext : EduppleDbContext
        { 

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                var userManager = services.GetRequiredService<UserManager<DOMAIN.Entities.User>>();
                var roleManager = services.GetRequiredService<RoleManager<DOMAIN.Entities.Role>>();
                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
                    var retry = Policy.Handle<Exception>().WaitAndRetry(new[]
                    {
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(15),
            });

                    retry.Execute(() =>
                    {
                        if (drop) context.Database.EnsureDeleted();
                        context.Database.Migrate();
                        if (drop) context.EnsureSeeded(userManager, roleManager);
                       
                    });
                    logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
                }
            }

            return host;
        }
    }
}
