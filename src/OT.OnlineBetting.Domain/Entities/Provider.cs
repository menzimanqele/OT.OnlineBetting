using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class Provider :IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}