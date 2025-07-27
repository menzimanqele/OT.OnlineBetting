using Microsoft.Extensions.DependencyInjection;
using OT.OnlineBetting.Application.Commands.CreateWager;
using OT.OnlineBetting.Application.Interfaces;

namespace OT.OnlineBetting.Application.Extensions.IoC;

public static class ApplicationExtensions
{
    public static void AddApplicationConfigurations(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<ICommandHandler<CreateWagerCommand>, CreateWagerHandler>();
    }
}