using MediatR;
using Track.Application.Dtos;
using Track.Application.TrackFoods.V1.Commands.InsertUserTrackFood;
using Track.Application.TrackFoods.V1.Commands.InsertUserTrackFoodByCalorie;
using Track.Application.TrackFoods.V1.Commands.SoftDeleteTrackFood;
using Track.Application.TrackFoods.V1.Commands.UpdateUserTrackFood;
using Track.Application.TrackFoods.V1.Queries.GetIntakeNutrients;
using Track.Application.TrackFoods.V1.Queries.GetUserTrackByDate;
using Track.Application.TrackFoods.V1.Queries.GetUserTrackById;
using Track.Application.TrackFoods.V1.Queries.GetUserTrackByMeal;
using Track.Application.TrackFoods.V1.Queries.GetUserTrackFoodHistory;
using Track.Domain.Enums;

namespace Track.Api.Controllers;

[ApiVersion("1")]
public class TrackFoodController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TrackFoodController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-track-food-by-meal")]
    public async Task<ActionResult> GetUserTrackByMeal(FoodMeal meal, DateTime dateTime,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserTrackByMealCommand(_currentUserService.UserId!, meal, dateTime),
            cancellationToken);
        return Ok(new ApiResult<List<UserTrackFoodDto>>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-track-food-by-date")]
    public async Task<ActionResult> GetUserTrackByDate(DateTime dateTime,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserTrackByDateCommand(dateTime, _currentUserService.UserId!),
            cancellationToken);
        return Ok(new ApiResult<List<UserTrackFoodDto>>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-track-food-by-id")]
    public async Task<ActionResult> GetTrackFoodById(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserTrackByIdCommand(id),
            cancellationToken);
        return Ok(new ApiResult<UserTrackFoodDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    #region DeletedApi

    // [HasPermission(PermissionsConstants.PostUserTrackSpecification)]
    // [HttpGet("get-in-take-nutrients")]
    // public async Task<ActionResult> GetIntakeNutrients(DateTime dateTime,
    //     CancellationToken cancellationToken)
    // {
    //     var result = await _mediator.Send(new GetIntakeNutrientsCommand(_currentUserService.UserId!, dateTime),
    //         cancellationToken);
    //     return Ok(new ApiResult<UserTrackNutrientDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    // }
    // [HasPermission(PermissionsConstants.PostUserTrackSpecification)]
    // [HttpGet("get-nutrients-report")]
    // public async Task<ActionResult> GetNutrientsReport(int userId, int days,
    //     CancellationToken cancellationToken)
    // {
    //     var result = await _mediator.Send(new GetIntakeNutrientsCommand(_currentUserService.UserId!, DateTime:new DateTime()),
    //         cancellationToken);
    //     return Ok(new ApiResult<UserTrackNutrientDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    // }sssss

    #endregion

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("user-track-food-history")]
    public async Task<ActionResult> GetUserTrackFoodHistory(int days,
        CancellationToken cancellationToken)
    {
        var dateTime = Convert.ToDateTime(DateTime.Now.AddDays(-days).ToString("MM/dd/yyyy"));
        var result = await _mediator.Send(new GetUserTrackFoodHistoryQuery(_currentUserService.UserId!, dateTime));
        return Ok(new ApiResult<List<UserTrackFoodDto>>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("create-user-track-food-by-calorie")]
    public async Task<ActionResult> PostByCalorie([FromBody] InsertUserTrackFoodByCalorieCommand request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.UserId!;
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<UserTrackFoodDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("insert-user-track-food")]
    public async Task<ActionResult> Post(InsertUserTrackFoodCommand request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.UserId!;
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<UserTrackFoodDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPatch("update-user-track-food")]
    public async Task<ActionResult> Put(UpdateUserTrackFoodCommand request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.UserId!;
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<UserTrackFoodDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpDelete("soft-delete-track-food")]
    public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteTrackFoodCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success, true));
    }
}