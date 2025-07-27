using MediatR;
using Microsoft.AspNetCore.Mvc;
using OT.OnlineBetting.Application.Commands.CreateWager;
using OT.OnlineBetting.Application.Interfaces;
using OT.OnlineBetting.Application.Queries.Wager;

namespace OT.OnlineBetting.Api.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController   : ControllerBase
    {
        /// <summary>
        /// Create player casino wager
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("casinoWager")]
        public async Task<IActionResult> Post([FromServices] ICommandHandler<CreateWagerCommand> handler, [FromBody] CreateWagerCommand command, CancellationToken cancellationToken)
        {
            await handler.HandleAsync(command,cancellationToken);
            return Ok();
        }
        
        /// <summary>
        /// Get latest casino wagers for a specific player.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="playerId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{playerId}/casino")]
        public async Task<IActionResult> GetPlayerWagers([FromServices] IMediator mediator, Guid playerId,
            [FromQuery] int pageSize = 10,
            [FromQuery] int page = 1,
            CancellationToken cancellationToken = default)
        {
            var res = await  mediator.Send(new GetWagersByPlayerQuery(playerId, page, pageSize), cancellationToken);
            return Ok(res);
        }

        /// <summary>
        /// Get top players based on total spending. Highest to Lowest.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="count"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("topSpenders")]
        public async Task<IActionResult> TopSpenders([FromServices] IMediator mediator,[FromQuery] int count = 10, CancellationToken cancellationToken = default)
        {
            var res = await  mediator.Send(new GetTopSpendersQuery(count), cancellationToken);
            return Ok(res);
        }
    }
}
