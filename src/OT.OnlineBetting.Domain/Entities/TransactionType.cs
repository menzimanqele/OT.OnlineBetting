using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class TransactionType : IBaseEntity<Guid>
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}