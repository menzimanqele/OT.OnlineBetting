using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class Country : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string Code { get; set; }
}