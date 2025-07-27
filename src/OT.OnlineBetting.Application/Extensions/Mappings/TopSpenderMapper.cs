using OT.OnlineBetting.Application.DTOs;
using OT.OnlineBetting.Domain.Entities;

namespace OT.OnlineBetting.Application.Extensions.Mappings;

public static class TopSpenderMapper
{
    public static TopSpenderDto MapToDto(this TopSpender model)
    {
        ArgumentNullException.ThrowIfNull(model);
        
        return new TopSpenderDto
        {
            TotalAmountSpend = model.TotalAmountSpend,
            AccountId = model.AccountId,
            UserName = model.UserName,
        };
    }
}