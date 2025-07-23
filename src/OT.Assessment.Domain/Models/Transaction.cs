using OT.Assessment.Domain.Interfaces;

namespace OT.Assessment.Domain.Models;

public class Transaction : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public Decimal Amount { get; set; }
    public short TransactionType { get; set; }
    
}