using Common.Enums.Foods;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Track.Application.Dtos;
using Track.Application.FavoriteMeals.V1.Commands.DeleteFavoriteMealFood;
using Track.Application.FavoriteMeals.V1.Commands.InsertFavoriteMeals;
using Track.Application.FavoriteMeals.V1.Commands.PartialUpdateFavoriteMeals;
using Track.Application.FavoriteMeals.V1.Commands.SoftDeleteFavoriteMeal;
using Track.Application.FavoriteMeals.V1.Queries.GetAllFavoriteMeals;
using Track.Application.FavoriteMeals.V1.Queries.GetByMealFavoriteMeal;

namespace Track.Api.Controllers;

[ApiVersion("1")]
public class FavoriteMealController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public FavoriteMealController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-favorite-by-meal")]
    public async Task<ActionResult> GetUserFavoriteByMeal(FoodMealEvent meal, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByMealFavoriteMealQuery
            {
                UserId = _currentUserService.UserId!,
                Meal = meal
            },
            cancellationToken);
        return Ok(new ApiResult<List<TrackMealDto>>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-all-user-favorite-meal")]
    public async Task<ActionResult> GetAllUserFavoriteMeal(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllTrackMealQuery
            {
                UserId = _currentUserService.UserId!
            },
            cancellationToken);
        return Ok(new ApiResult<List<TrackMealDto>>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("insert-user-favorite-meal")]
    public async Task<ActionResult> InsertUserFavoriteMeal(InsertFavoriteMealsCommand request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.UserId!;
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<TrackMealDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPatch("add-food-user-favorite-meal")]
    public async Task<ActionResult> AddFoodUserFavoriteMeal(FoodMealEvent meal, [FromQuery] List<FoodMealDto> foods,
        decimal calorieValue, string imageUri,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new PatchFavoriteMealFoodsCommand(_currentUserService.UserId!, meal, foods, calorieValue, imageUri),
            cancellationToken);
        return Ok();
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpDelete("soft-delete-user-track-meal")]
    public async Task<ActionResult> SoftDeleteUserTrackMeal(string id,                                                                                                                                                                                      
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteFavoriteMealCommand(id), cancellationToken);
        return Ok();
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpDelete("delete-food-user-track-meal")]
    public async Task<ActionResult> DeleteFoodUserTrackMeal(FoodMealEvent meal, string foodId,
        CancellationToken cancellationToken)
    {   
        await _mediator.Send(new DeleteFavoriteMealFoodCommand(foodId, meal, _currentUserService.UserId!),
            cancellationToken);
        return Ok();
    }
}