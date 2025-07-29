using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Infrastructure.Repositories;

public class Repository<T, TKey>(ILogger<Repository<T, TKey>> logger, DataConnection dbConnection)
    : IRepository<T, TKey>
    where T : class, IBaseEntity, IBaseEntity<TKey>
{
    
    public  IEnumerable<T> GetAllAsync() 
    {
        return  dbConnection.GetTable<T>();
    }

    public async Task<T?> GetByIdAsync(TKey id) 
    {
        return await dbConnection.GetTable<T>().FirstOrDefaultAsync(x=>x.Id != null && x.Id.Equals(id));
    }

    public async Task<int> AddSync(T entity)
    {
       var numberOfAffectedRows =  await dbConnection.InsertAsync(entity);
       return numberOfAffectedRows;
    }

    public IEnumerable<T> FindByConditionAsync(Func<T, bool> predicate)
    {
        var result =  dbConnection.GetTable<T>();
        return result.Where(predicate);
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        var deletedCount = await  dbConnection.GetTable<T>().DeleteAsync(x => x.Id!.Equals(id));
        return deletedCount > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var updated = await  dbConnection.UpdateAsync(entity);
        return updated > 0;
    }
}
