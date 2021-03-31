using Microsoft.Extensions.Logging;


namespace EDUPPLE.INFRASTRUCTURE.Persistence
{
    public class LoggingFactory
    {
        public static ILoggerFactory LoggerFactory { get; private set; }

        static LoggingFactory()
        {

            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == Microsoft.EntityFrameworkCore.DbLoggerCategory.Database.Command.Name &&
                        level == LogLevel.Information)
                    .AddDebug();
            });
        }
    }
}
