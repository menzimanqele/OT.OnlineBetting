using OT.OnlineBetting.Domain.Entities;

namespace OT.OnlineBetting.Domain.Interfaces;

public interface IWagerRepository : IRepository<Wager, Guid>, IRepository
{
    Task<(List<Wager> data, int total)>  GetWagersByProductId(Guid playerId, int page =1 , int pageSize = 10, CancellationToken cancellationToken = default);

    Task<List<TopSpender>> GetTopSpenders(int count,
        CancellationToken cancellationToken = default);
}