using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class Game : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ProviderId { get; set; }
    public bool IsActive { get; set; }
}