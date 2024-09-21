using MediatR;
using Microsoft.AspNetCore.Mvc;
using Track.Application.Dtos;
using Track.Application.TrackStep;
using Track.Application.TrackStep.V1.Commands.InsertUserTrackSteps;
using Track.Application.TrackStep.V1.Commands.SoftDeleteUserSteps;
using Track.Application.TrackStep.V1.Commands.UpdateUserSteps;
using Track.Application.TrackStep.V1.Commands.UpdateUserStepsByAppId;
using Track.Application.TrackStep.V1.Queries.GetUserSteps;
using Track.Application.TrackStep.V1.Queries.GetUserStepsHistory;

namespace Track.Api.Controllers;

[ApiVersion("1")]
public class TrackStepsController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TrackStepsController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-steps-history")]
    public async Task<List<UserStepsDto>> GetUserStepsHistory(int days,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetUserStepsHistoryQuery(_currentUserService.UserId, days),
            cancellationToken);
    }
    
    [PermissionAuthorize(PermissionsConstants.FullAccess)]
    [HttpGet]
    public async Task<List<UserStepsDto>> GetUserSteps(DateTime dateTime, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetUserStepsQuery(_currentUserService.UserId, dateTime),
            cancellationToken);
    }


    // [HttpGet("GetAsyncBydate")]
    // public async Task<List<UserStepsDto>> GetByDate(string userId, int daysCount, CancellationToken cancellationToken)
    // {
    //     return await _mediator.Send(new GetByDateQuery(userId,daysCount),
    //         cancellationToken);
    // }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("user-track-steps")]
    public async Task<ApiResult> InsertUserSteps(InsertUserTrackStepsCommand request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.UserId;
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPatch("user-track-steps-by-app-id")]
    public async Task<ApiResult> UpdateUserStepsByAppId(UpdateUserStepsByAppIdCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPatch("user-track-steps")]
    public async Task<ApiResult> UpdateUserSteps(UpdateUserStepsCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpDelete("soft-delete-user-steps")]
    public async Task<ApiResult> SoftDeleteUserSteps(string trackStepsId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteUserStepsCommand(trackStepsId), cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
}