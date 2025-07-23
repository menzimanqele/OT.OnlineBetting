using OT.Assessment.Domain.Interfaces;

namespace OT.Assessment.Domain.Models;

public class Country : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string Code { get; set; }
}