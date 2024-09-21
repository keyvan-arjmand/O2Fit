using Market.Application.AppVersions.V1.Commands.InsertAppVersion;
using Market.Application.AppVersions.V1.Commands.SoftDeleteAppVersion;
using Market.Application.AppVersions.V1.Commands.UpdateAppVersion;
using Market.Application.AppVersions.V1.Queries.GetAllAppVersion;
using Market.Application.AppVersions.V1.Queries.GetAppVersion;
using Market.Application.Common.Models;
using Market.Application.Dtos;
using Market.Domain.Enums;
using MediatR;

namespace Market.Api.Controllers;

[ApiVersion("1")]
public class AppVersionController : BaseApiController
{
    private readonly IMediator _mediator;

    public AppVersionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-all-app-version")]
    public async Task<ActionResult<PaginationResult<AppVersionDto>>> GetAll(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllAppVersionQuery(page, pageSize), cancellationToken);
    }

    [HttpGet("get-app-version")]
    public async Task<ActionResult<AppVersionDto>> GetAppVersion(MarketType marketType, string currentAppVersion,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAppVersionQuery(marketType, currentAppVersion), cancellationToken);
    }

    [HttpPost("insert-app-version")]
    public async Task<ActionResult> Post(InsertAppVersionCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPut("update-app-version")]
    public async Task<ActionResult> Update(UpdateAppVersionCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpDelete("soft-delete-app-version")]
    public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteAppVersionCommand(id), cancellationToken);
        return Ok();
    }
}