using OT.Assessment.Domain.Interfaces;

namespace OT.Assessment.Domain.Models;

public class Player : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public Guid CountryId { get; set; }
}