using OT.Common.EventBus.Bus;
using Microsoft.Extensions.Configuration;
using OT.BackGroundWorker.Consumer;
using OT.OnlineBetting.Consumer;
using OT.OnlineBetting.Shared.EventHandlers;
using OT.OnlineBetting.Shared.Events;
using OT.OnlineBetting.Shared.IoC;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger(); // Early logger for startup

try
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.Services.AddSingleton<MonitorLoop>();
    builder.Services.AddHostedService<QueuedHostedService>();
    builder.Services.AddSingleton<IBackgroundTaskQueue>(_ =>
    {
        if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
        {
            queueCapacity = 100;
        }
        return new DefaultBackgroundTaskQueue(queueCapacity);
    });

    builder.Services.RegisterSharedServices(builder.Configuration);
    var host = builder.Build();
    var bus = host.Services.GetRequiredService<IEventBus>();
    bus.Subcribe<WagerCreatedEvent, WagerCreatedEventHandler>();
    Log.Information("Host is starting");
    await host.RunAsync();
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Fatal(exception, "Host terminated unexpectedly! '{ErrorMessage}'", exception.Message);
}finally
{
    Log.CloseAndFlush();
}