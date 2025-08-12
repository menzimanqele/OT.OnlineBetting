using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Application.Extensions.Mappings;
using OT.OnlineBetting.Domain.Interfaces;
using OT.OnlineBetting.Application.DTOs;

namespace OT.OnlineBetting.Application.Queries.Wager;

public class GetWagersByPlayerQuery : IRequest<PaginatedDto<WagerDto>>
{
    public Guid PlayerId { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    
    public GetWagersByPlayerQuery(Guid playerId, int page =1 , int pageSize = 10)
    {
        PlayerId = playerId;
        PageNumber = page;
        PageSize = pageSize;
    }

    public class GetWagersByPlayerQueryValidation : AbstractValidator<GetWagersByPlayerQuery>
    {
        public GetWagersByPlayerQueryValidation()
        {
            RuleFor(x => x.PlayerId).NotEmpty();
        }
    }
    
    public class GetWagersByPlayerQueryHandler(ILogger<GetWagersByPlayerQueryHandler> logger,IUnitOfWork unitOfWork) : IRequestHandler<GetWagersByPlayerQuery,PaginatedDto<WagerDto>>
    {
        public async Task<PaginatedDto<WagerDto>> Handle(GetWagersByPlayerQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Attempting to get all wager by player {request.PlayerId}");
            
            var results = await unitOfWork.GetRepository<IWagerRepository>().GetWagersByProductId(request.PlayerId, request.PageNumber, request.PageSize, cancellationToken);

            if (results.total > 0)
            {
                logger.LogInformation($"Found {results.data.Count} records");

                var wagerDtos = results.data.Select(x => x.MapToDto()).ToList();

                return new PaginatedDto<WagerDto>
                {
                    Data = wagerDtos,
                    PageSize = request.PageSize,
                    Page = request.PageNumber,
                    Total = results.total
                };
            }
            
            logger.LogWarning($"No records found for player {request.PlayerId}");
            
            return new PaginatedDto<WagerDto>
            {
                Data = [],
                PageSize = request.PageSize,
                Page = request.PageNumber,
                Total = results.total
            };
        }
    }
}