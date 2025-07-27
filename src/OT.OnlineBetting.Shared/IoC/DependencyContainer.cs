using OT.Common.EventBus.RabbitMQ.Implementation;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using MediatR;
using OT.Common.EventBus.Bus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Domain.Interfaces;
using OT.OnlineBetting.Infrastructure.Persisent;
using OT.OnlineBetting.Infrastructure.Repositories;
using OT.OnlineBetting.Shared.EventHandlers;
using OT.OnlineBetting.Shared.Events;

namespace OT.OnlineBetting.Shared.IoC;

public static class DependencyContainer
{
    public static void RegisterSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var scopeFactory = sp.GetService<IServiceScopeFactory>();
                return new EventBusRabbitMQ(sp.GetService<ILogger<EventBusRabbitMQ>>(),sp.GetService<IMediator>(), scopeFactory);
            }
        );
        
        services.AddLinqToDBContext<AppDbContext>((provoider, options) =>
            options.UseSqlServer(configuration.GetDbConnectionString())
                .UseDefaultLogging(provoider));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWagerRepository, WagerRepository>();

        //Subscriptions 
        services.AddTransient<WagerCreatedEventHandler>();
        services.AddTransient<IEventHandler<WagerCreatedEvent>, WagerCreatedEventHandler>();
    }

    private static string GetDbConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("DatabaseConnection");
    }
}