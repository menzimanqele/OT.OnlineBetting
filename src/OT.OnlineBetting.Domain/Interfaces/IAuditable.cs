namespace OT.OnlineBetting.Domain.Interfaces;

public interface IAuditable
{
    public string CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string? UpdatedBy { get; set; }
}