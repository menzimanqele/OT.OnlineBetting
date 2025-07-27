namespace OT.OnlineBetting.Domain.Interfaces;

public interface IUnitOfWork
{
    TRepository GetRepository<TRepository>() where TRepository : class, IRepository;
}