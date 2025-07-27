using OT.Common.EventBus.Bus;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Domain.Entities;
using OT.OnlineBetting.Domain.Interfaces;
using OT.OnlineBetting.Shared.Events;

namespace OT.OnlineBetting.Shared.EventHandlers;

public class WagerCreatedEventHandler(ILogger<WagerCreatedEvent> logger, IUnitOfWork unitOfWork) : IEventHandler<WagerCreatedEvent>
{
    public async Task Handle(WagerCreatedEvent @event)
    {
        logger.LogInformation($"Wager Created Event: {@event.Id}"); 
        await unitOfWork.GetRepository<IWagerRepository>().AddSync(@event.ToWagerModel());
        logger.LogInformation($"Successfully created wager with id: {@event.Id}");
    }
}


public static class WagerCreatedEventMapping
{
    public static Wager ToWagerModel(this WagerCreatedEvent @event)
    {
        return new Wager
        {
            Id = @event.Id,
            Amount = @event.Amount,
            BrandId =  @event.BrandId,
            CreatedBy = @event.CreatedBy,
            DateCreated = @event.DateCreated,
            Duration = @event.Duration,
            ExternalReferenceId = @event.ExternalReferenceId,
            TransactionId = @event.TransactionId,
            NumberOfBets = @event.NumberOfBets,
            TransactionTypeId = Guid.Parse("66666666-0000-0000-0000-000000000002"), // @event.TransactionTypeId,
            GameId = Guid.Parse("55555555-0000-0000-0000-000000000001"), //@event.GameId
            PlayerId =Guid.Parse("22222222-0000-0000-0000-000000000002")  //@event.PlayerId,
        };
    }
}