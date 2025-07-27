namespace OT.OnlineBetting.Application.DTOs;

public class WagerDto
{
    public Guid WagerId { get; init; }
    public string Game { get; init; }
    public string Provier { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreateDate { get; init; }
}