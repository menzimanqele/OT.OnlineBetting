namespace OT.OnlineBetting.Domain.Interfaces;

public interface IRepository
{
    
}

public interface IRepository<T,TKey> where T : class, IBaseEntity, IBaseEntity<TKey>
{
    IEnumerable<T> GetAllAsync();
    Task<T?> GetByIdAsync(TKey id);      
    Task AddSync(T entity);
    IEnumerable<T> FindByConditionAsync(Func<T, bool> predicate);
    Task<bool> DeleteAsync(TKey id);
    Task<bool> UpdateAsync(T entity);
}