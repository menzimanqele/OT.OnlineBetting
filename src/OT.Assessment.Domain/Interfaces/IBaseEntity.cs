namespace OT.Assessment.Domain.Interfaces;

public interface IBaseEntity<TKey> 
{
    TKey Id { get; set; }
}