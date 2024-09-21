using MediatR;
using Microsoft.AspNetCore.Mvc;
using Track.Application.Dtos;
using Track.Application.FavoriteRecipes.V1.Commands.InsertTrackRecipe;
using Track.Application.FavoriteRecipes.V1.Commands.SoftDeleteFavoriteRecipe;
using Track.Application.FavoriteRecipes.V1.Queries.GetAllFavoriteRecipe;
using Track.Application.FavoriteRecipes.V1.Queries.GetByFoodIdFavoriteRecipe;

namespace Track.Api.Controllers;

[ApiVersion("1")]
public class FavoriteRecipeController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public FavoriteRecipeController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-user-favorite-recipe-by-food-id")]
    public async Task<ActionResult> GetUserFavoriteRecipeByFoodId(string foodId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByFoodIdFavoriteRecipeQuery
        {
            FoodId = foodId,
            UserId = _currentUserService.UserId!
        }, cancellationToken);
        return Ok(new ApiResult<TrackRecipeDto>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-all-favorite-recipe")]
    public async Task<ActionResult> GetAllFavoriteRecipe(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllFavoriteRecipeQuery(_currentUserService.UserId!),
            cancellationToken);
        return Ok(new ApiResult<List<TrackRecipeDto>>(result, string.Empty, ApiResultStatusCode.Success, true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("insert-favorite-recipe")]
    public async Task<ActionResult> InsertFavoriteRecipe(InsertFavoriteRecipeCommand request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.UserId!;
        await _mediator.Send(request, cancellationToken);
        return Ok();
    }
    
    // [HasPermission(PermissionsConstants.PostUserTrackSpecification)]
    // [HttpPatch("add-food-track-recipe")]
    // public async Task<ActionResult> AddFoodTrackRecipe(FoodMeal meal, string foodId,
    //     CancellationToken cancellationToken)
    // {
    //     await _mediator.Send(new PatchTrackRecipeCommand(_currentUserService.UserId!, meal, foodId),
    //         cancellationToken);
    //     return Ok();
    // }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpDelete("soft-delete-favorite-recipe")]
    public async Task<ActionResult> SoftDeleteFavoriteRecipe(string id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteFavoriteRecipeCommand(id),
            cancellationToken);
        return Ok();
    }
}