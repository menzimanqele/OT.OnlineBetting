using OT.OnlineBetting.Shared.Events;

namespace OT.OnlineBetting.Application.Extensions.Mappings;

public static class CreateWagerCommandMapper
{
    public static WagerCreatedEvent MapToEvent(this Commands.CreateWager.CreateWagerCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        
        return new WagerCreatedEvent
        {
            Id = command.Id,
            Amount = command.Amount,
            BrandId = command.BrandId,
            CreatedBy = command.CreatedBy,
            DateCreated = command.DateCreated,
            Duration = command.Duration,
            ExternalReferenceId = command.ExternalReferenceId,
            NumberOfBets = command.NumberOfBets,
            TransactionId = command.TransactionId,
            TransactionTypeId = command.TransactionTypeId,
            GameId = command.GameId,
            UserId = command.UserId,
        };
    }
}