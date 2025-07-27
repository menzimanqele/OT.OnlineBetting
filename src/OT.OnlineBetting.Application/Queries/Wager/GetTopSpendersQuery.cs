using MediatR;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Application.Extensions.Mappings;
using OT.OnlineBetting.Domain.Interfaces;
using OT.OnlineBetting.Application.DTOs;

namespace OT.OnlineBetting.Application.Queries.Wager;

public class GetTopSpendersQuery(int count = 10) : IRequest<List<TopSpenderDto>>
{
    public int Count { get; } = count;

    public class GetTopSpendersQueryHandler(ILogger<GetTopSpendersQueryHandler> logger, IUnitOfWork unitOfWork)
        : IRequestHandler<GetTopSpendersQuery, List<TopSpenderDto>>
    {
        public async Task<List<TopSpenderDto>> Handle(GetTopSpendersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Attempting to get Top Spenders");

            var results = await unitOfWork.GetRepository<IWagerRepository>().GetTopSpenders(request.Count);

            if (results?.Count > 0)
            {
                logger.LogInformation($"Found {results.Count} Top Spenders");
                return results.Select(x => x.MapToDto()).ToList();
            }

            logger.LogWarning("No Top Spenders Found");
            return [];
        }
    }
}