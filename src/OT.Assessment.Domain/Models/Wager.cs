using OT.Assessment.Domain.Interfaces;

namespace OT.Assessment.Domain.Models;

public class Wager : IBaseEntity<Guid>, IAuditable
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public Guid TransactionId { get; set; } 
    public Guid BrandId { get; set; }
    public Guid UserId { get; set; }  
    public Guid ExternalReferenceId { get; set; }
    public int NumberOfBets { get; set; }
    public long Duration { get; set; }
    public string CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string? UpdatedBy { get; set; }
}   