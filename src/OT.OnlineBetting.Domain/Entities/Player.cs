using LinqToDB.Mapping;
using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class Player : IBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public Guid CountryId { get; set; }
    [NotColumn]
    public Account? Account  { get; set; }
}