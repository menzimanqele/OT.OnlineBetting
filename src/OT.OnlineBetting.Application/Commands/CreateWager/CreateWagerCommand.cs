namespace OT.OnlineBetting.Application.Commands.CreateWager;

public class CreateWagerCommand 
{
    public Guid Id { get; set; }
    public required Guid GameId { get; set; }
    public required Guid TransactionId { get; set; } 
    public required Guid BrandId { get; set; }
    public required Guid UserId { get; set; }  
    public required Guid ExternalReferenceId { get; set; }
    public required int NumberOfBets { get; set; }
    public long Duration { get; set; }
    public required Guid TransactionTypeId { get; set; }
    public required decimal Amount { get; set; }
    public string CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
}