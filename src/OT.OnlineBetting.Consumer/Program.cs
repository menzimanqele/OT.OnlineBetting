using OT.Common.EventBus.Bus;
using Microsoft.Extensions.Configuration;
using OT.BackGroundWorker.Consumer;
using OT.OnlineBetting.Consumer;
using OT.OnlineBetting.Shared.EventHandlers;
using OT.OnlineBetting.Shared.Events;
using OT.OnlineBetting.Shared.IoC;

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


//var logger = builder.Services.GetRequiredService<ILogger<Program>>();
//logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
var host = builder.Build();

var bus = host.Services.GetRequiredService<IEventBus>();
bus.Subcribe<WagerCreatedEvent, WagerCreatedEventHandler>();

host.Run();

//logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


