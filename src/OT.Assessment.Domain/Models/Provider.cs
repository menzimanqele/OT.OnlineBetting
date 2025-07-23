using OT.Assessment.Domain.Interfaces;

namespace OT.Assessment.Domain.Models;

public class Provider :IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}