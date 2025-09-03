using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OT.OnlineBetting.Application.Extensions.Mappings;
using OT.OnlineBetting.Domain.Interfaces;
using OT.OnlineBetting.Application.DTOs;

namespace OT.OnlineBetting.Application.Queries.Wager;

public class GetWagersByPlayerQuery : IRequest<PaginatedDto<WagerDto>>
{
    [FromRoute]
    public Guid PlayerId { get; init; }

    [FromQuery] public int PageNumber { get; init; } = 1;
    [FromQuery] public int PageSize { get; init; } = 10;
    
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
                    Page = new PageDto
                    {
                        Number = request.PageNumber,
                        Size = request.PageSize,
                    },
                    Total = results.total
                };
            }
            
            logger.LogWarning($"No records found for player {request.PlayerId}");
            
            return new PaginatedDto<WagerDto>
            {
                Data = [],
                Page = new PageDto
                {
                    Number = request.PageNumber,
                    Size = request.PageSize,
                },
                Total = results.total
            };
        }
    }
}