using Microsoft.AspNetCore.Builder;
using Serilog;

namespace OT.OnlineBetting.Shared.IoC;

public static class LoggingBuilderExtensions
{
    public static void AddLoggingSupport(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.WithEnvironmentName()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File($"logs/_{builder.Environment.ApplicationName}.log",  //TOOD : read log path from config
                    rollingInterval: RollingInterval.Hour); 
        });
        
    }
}