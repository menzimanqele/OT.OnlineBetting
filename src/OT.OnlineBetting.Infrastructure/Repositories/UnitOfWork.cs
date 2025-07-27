using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Domain.Interfaces;

namespace OT.OnlineBetting.Infrastructure.Repositories;

public class UnitOfWork(IServiceProvider serviceProvider, ILogger<UnitOfWork> logger)
    : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly ILogger<UnitOfWork> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public TRepository GetRepository<TRepository>() where TRepository : class, IRepository
    {
        var repo = _serviceProvider.GetRequiredService<TRepository>();
        
        return repo;
    }
}