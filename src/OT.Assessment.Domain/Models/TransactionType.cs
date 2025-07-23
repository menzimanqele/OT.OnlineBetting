using OT.Assessment.Domain.Interfaces;

namespace OT.Assessment.Domain.Models;

public class TransactionType : IBaseEntity<Guid>
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}