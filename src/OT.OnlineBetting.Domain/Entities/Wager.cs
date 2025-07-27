using LinqToDB.Mapping;
using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Domain.Entities;

public class Wager : IBaseEntity<Guid>, IAuditable
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    /// <summary>
    /// Ensures one-to-one mapping per wager.
    /// </summary>
    public Guid TransactionId { get; set; } 
    public Guid BrandId { get; set; }
    public Guid PlayerId { get; set; }  
    /// <summary>
    /// For tracing or correlation, 
    /// </summary>
    public Guid ExternalReferenceId { get; set; }
    public int NumberOfBets { get; set; }
    public long Duration { get; set; }
    public Guid TransactionTypeId { get; set; }
    public decimal Amount { get; set; }
    public string CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string? UpdatedBy { get; set; }
    [NotColumn]
    public string?  Game { get; set; }
    [NotColumn] 
    public string? Provider { get; set; }
}   