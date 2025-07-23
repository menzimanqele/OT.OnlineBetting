using OT.Assessment.Domain.Interfaces;

namespace OT.Assessment.Domain.Models;

public class TransactionType : IBaseEntity<short>
{
    public string Name { get; set; }
    public short Id { get; set; }
}