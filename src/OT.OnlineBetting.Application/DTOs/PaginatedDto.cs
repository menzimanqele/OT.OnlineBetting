namespace OT.OnlineBetting.Application.DTOs;

public class PaginatedDto<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public PageDto Page { get; set; }
    public int Total { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)Total / Page.Size);
}