using FluentValidation;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Application.Extensions.Mappings;
using OT.Common.EventBus.Bus;
using OT.OnlineBetting.Application.Interfaces;

namespace OT.OnlineBetting.Application.Commands.CreateWager;

public class CreateWagerHandler (ILogger<CreateWagerHandler> logger, IEventBus eventBus) : ICommandHandler<CreateWagerCommand>
{
    public class CreateWagerCommandValidator : AbstractValidator<CreateWagerCommand>
    {
        public CreateWagerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.GameId).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.TransactionId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.NumberOfBets).GreaterThan(0);
            RuleFor(x => x.Duration).GreaterThan(0);
            RuleFor(x => x.TransactionTypeId).NotEmpty();
            RuleFor(x => x.ExternalReferenceId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.DateCreated).NotEmpty();
            RuleFor(x => x.CreatedBy).NotEmpty();
        }
    }
    
    public async Task HandleAsync(CreateWagerCommand command, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("Create Wager request has been cancelled");
            return;
        }
        
        eventBus.Publish(command.MapToEvent());
        logger.LogInformation("Create Wager request has been published of processing");
    }
}