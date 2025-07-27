namespace OT.OnlineBetting.Domain.Interfaces;

public interface IBaseEntity<TKey> : IBaseEntity
{
    TKey Id { get; set; }
}

public interface IBaseEntity
{
    
}