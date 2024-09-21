using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using Track.Application.Dtos;
using Track.Application.TrackSleeps.V1.Commands.InsertTrackSleep;
using Track.Application.TrackSleeps.V1.Commands.SoftDeleteTrackSleep;
using Track.Application.TrackSleeps.V1.Commands.UpdateTrackSleep;
using Track.Application.TrackSleeps.V1.Queries.GetByDateTrackSleep;
using Track.Application.TrackSleeps.V1.Queries.GetTrackSleep;
using Track.Application.TrackSleeps.V1.Queries.GetUserSleepHistory;

namespace Track.Api.Controllers;

[ApiVersion("1")]
public class TrackSleepController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TrackSleepController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-track-sleep")]
    public async Task<ActionResult> GetTrackSleep(int days,
        CancellationToken cancellationToken)
    {
        var result =
            await _mediator.Send(new GetTrackSleepQuery(_currentUserService.UserId!, days),
                cancellationToken);
        return Ok(new ApiResult<UserTrackSleepAverageDto>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-by-date-track-sleep")]
    public async Task<ActionResult> GetByDate(int days,
        CancellationToken cancellationToken)
    {
        var result =
            await _mediator.Send(new GetByDateTrackSleepQuery(_currentUserService.UserId!, DateTime.Now.AddDays(-days)),
                cancellationToken);
        return Ok(new ApiResult<UserTrackSleepDto>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }


    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-history-user-track-sleep")]
    public async Task<ActionResult> GetUserHistory(int days,
        CancellationToken cancellationToken)
    {
        var result =
            await _mediator.Send(new GetUserSleepHistoryQuery(_currentUserService.UserId!, DateTime.Now.AddDays(-days)),
                cancellationToken);
        return Ok(new ApiResult<List<UserTrackSleepDto>>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("insert-track-sleep")]
    public async Task<ActionResult> InsertTrackSleep(InsertTrackSleepCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<UserTrackSleepDto>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPut("update-track-sleep-command")]
    public async Task<ActionResult> UpdateTrackSleep(UpdateTrackSleepCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<UserTrackSleepDto>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpDelete("soft-delete-track-sleep")]
    public async Task<ActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteTrackSleepCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success,
            true));
    }
}