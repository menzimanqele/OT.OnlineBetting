using LinqToDB;
using LinqToDB.Data;

namespace OT.OnlineBetting.Infrastructure.Persisent;

public class AppDbContext : DataConnection
{
    public AppDbContext(DataOptions<AppDbContext> options): base(options.Options)
    {
        
    }
}