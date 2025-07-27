namespace OT.OnlineBetting.Application.DTOs;

public class TopSpenderDto
{
    public string UserName { get; init; }
    public decimal TotalAmountSpend { get; init; }
    public Guid AccountId { get; init; }
}