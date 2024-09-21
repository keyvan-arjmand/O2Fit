using Market.Application.Common.Models;
using Market.Application.Dtos;
using Market.Application.MarketMessages.V1.Commands.InsertMarketMessage;
using Market.Application.MarketMessages.V1.Commands.SoftDeleteMarketMessage;
using Market.Application.MarketMessages.V1.Commands.UpdateMarketMessage;
using Market.Application.MarketMessages.V1.Queries.GetAllMarketMessage;
using Market.Application.MarketMessages.V1.Queries.GetByDateMarketMessage;
using Market.Application.MarketMessages.V1.Queries.GetByIdMarketMessage;
using MediatR;

namespace Market.Api.Controllers;

[ApiVersion("1")]
public class MarketMessageController : BaseApiController
{
    private readonly IMediator _mediator;

    public MarketMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-all-market-message")]
    public async Task<ActionResult<PaginationResult<MarketMessageDto>>> GetAll(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllMarketMessageQuery(page, pageSize), cancellationToken);
    }

    [HttpGet("get-by-date-user")]
    public async Task<ActionResult<MarketMessageDto>> GetByDateUser(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetByDateMarketMessageQuery(), cancellationToken);
    }
    [HttpGet("get-by-id-market-message")]
    public async Task<ActionResult<MarketMessageDto>> GetById(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetByIdMarketMessageQuery(id), cancellationToken);
    }

    [HttpPost("insert-market-message")]
    public async Task<ActionResult> Post(InsertMarketMessageCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPut("update-market-message")]
    public async Task<ActionResult> Update(UpdateMarketMessageCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpDelete("soft-delete-market-message")]
    public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteMarketMessageCommand(id), cancellationToken);
        return Ok();
    }
}