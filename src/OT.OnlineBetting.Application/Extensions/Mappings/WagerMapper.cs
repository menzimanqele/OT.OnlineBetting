using OT.OnlineBetting.Application.DTOs;
using OT.OnlineBetting.Domain.Entities;

namespace OT.OnlineBetting.Application.Extensions.Mappings;

public static class WagerMapper
{
    public static WagerDto MapToDto(this Wager model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new WagerDto
        {
            WagerId = model.Id,
            Game = model.Game,
            Provier = model.Provider,
            Amount = model.Amount,
            CreateDate = model.DateCreated
        };
    }
}