using LinqToDB;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Domain.Entities;
using OT.OnlineBetting.Domain.Interfaces;
using OT.OnlineBetting.Infrastructure.Persisent;

namespace OT.OnlineBetting.Infrastructure.Repositories;

public class WagerRepository (ILogger<Repository<Wager,Guid>> logger,AppDbContext dbConnection )
    : Repository<Wager,Guid>(logger, dbConnection) , IWagerRepository
{
    public async Task<(List<Wager> data, int total)> GetWagersByProductId(Guid playerId, int page = 1, int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize <= 10 ? 10 : pageSize;
        var skip = (page - 1) * pageSize;

        await using var db = dbConnection;

        var query = from wager in db.GetTable<Wager>()
            join game in db.GetTable<Game>() on wager.GameId equals game.Id
            join provider in db.GetTable<Provider>() on game.ProviderId equals provider.Id
            where wager.PlayerId == playerId
            orderby wager.DateCreated descending
            select new Wager()
            {
                Id = wager.Id,
                Game = game.Name,
                Provider = provider.Name,
                Amount = wager.Amount,
                DateCreated = wager.DateCreated
            };

        var total = await query.CountAsync(cancellationToken);
        
        var data = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return (data, total);
    }
    
    public async Task<List<TopSpender>> GetTopSpenders(int count,
        CancellationToken cancellationToken = default)
    {
        count = count < 1 ? 10 : count;
        await using var db = dbConnection;
        
        var topSpenders = await (
                from wager in db.GetTable<Wager>()
                join player in db.GetTable<Player>() on wager.PlayerId equals player.Id
                join account in db.GetTable<Account>() on player.Id equals account.PlayerId
                group wager by new { account.Id, player.UserName } into g
                orderby g.Sum(x => x.Amount) descending
                select new TopSpender
                {
                    AccountId = g.Key.Id,
                    UserName = g.Key.UserName,
                    TotalAmountSpend = g.Sum(x => x.Amount)
                }
            )
            .Take(count)
            .ToListAsync(cancellationToken);

        
        return topSpenders;
    }
}