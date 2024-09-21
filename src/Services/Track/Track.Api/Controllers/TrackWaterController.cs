using MediatR;
using Microsoft.AspNetCore.Mvc;
using Track.Application.Dtos;
using Track.Application.FavoriteFoods.V1.Queries.GetFavoriteFoodByUserId;
using Track.Application.TrackWaters.V1.Commands.InsertTrackWater;
using Track.Application.TrackWaters.V1.Queries.GetTrackWaterByDate;
using Track.Application.TrackWaters.V1.Queries.GetUserTrackWaterHistory;

namespace Track.Api.Controllers;

[ApiVersion("1")]
public class TrackWaterController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TrackWaterController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-track-water-by-date")]
    public async Task<ActionResult<List<UserTrackWaterDto>>> GetTrackWaterByDate(DateTime? startDate, DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetTrackWaterByDateQuery(_currentUserService.UserId!, startDate, endDate),
            cancellationToken);
        return Ok(new ApiResult<List<UserTrackWaterDto>>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-track-water-history")]
    public async Task<ActionResult<List<UserTrackWaterDto>>> GetUserTrackWaterHistory(int days,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetUserTrackWaterHistoryQuery(_currentUserService.UserId!, DateTime.Now.AddDays(-days)),
            cancellationToken);
        return Ok(new ApiResult<List<UserTrackWaterDto>>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("insert-user-track-water")]
    public async Task<ActionResult<UserTrackWaterDto>> InsertTrackWater(InsertTrackWaterCommand request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.UserId!;
        var result = await _mediator.Send(request,
            cancellationToken);
        return Ok(new ApiResult<UserTrackWaterDto>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }
}