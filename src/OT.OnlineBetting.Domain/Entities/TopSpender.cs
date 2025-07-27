using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class TopSpender : IBaseEntity
{
    public string UserName { get; set; }
    public decimal TotalAmountSpend { get; set; }
    public Guid AccountId { get; set; }
}