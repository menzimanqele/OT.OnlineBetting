using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class Account : IBaseEntity<Guid>, IAuditable
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string? UpdatedBy { get; set; }
}