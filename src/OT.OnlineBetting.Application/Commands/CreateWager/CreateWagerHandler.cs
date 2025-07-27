using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Application.Extensions.Mappings;
using OT.Common.EventBus.Bus;
using OT.OnlineBetting.Application.Interfaces;

namespace OT.OnlineBetting.Application.Commands.CreateWager;

public class CreateWagerHandler (ILogger<CreateWagerHandler> logger, IEventBus eventBus) : ICommandHandler<CreateWagerCommand>
{
    public async Task HandleAsync(CreateWagerCommand command, CancellationToken cancellationToken)
    {
        // Check Transaction Id -> Must be unique
        if (cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("Create Wager request has been cancelled");
            return;
        }
        
        eventBus.Publish(command.MapToEvent());
        logger.LogInformation("Create Wager request has been published of processing");
    }
}