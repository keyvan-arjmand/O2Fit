using MediatR;
using Microsoft.AspNetCore.Mvc;
using Track.Application.Dtos;
using Track.Application.FavoriteFoods.V1.Commands.InsertFavoriteFood;
using Track.Application.FavoriteFoods.V1.Commands.SoftDeleteFavoriteFood;
using Track.Application.FavoriteFoods.V1.Queries.GetFavoriteFoodByUserId;

namespace Track.Api.Controllers;

[ApiVersion("1")]
public class FavoriteFoodController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public FavoriteFoodController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpGet("get-favorite-foodBy-user-id")]
    public async Task<ActionResult> GetFavoriteFoodByUserId(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetFavoriteFoodByUserIdQuery(_currentUserService.UserId!, page, pageSize, Language),
            cancellationToken);
        return Ok(new ApiResult<List<FavoriteFoodGetDto>>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [PermissionAuthorize(PermissionsConstants.FullAccess)]

    [HttpPost("insert-favorite-food")]
    public async Task<ActionResult> InsertFavoriteFood(string foodId, string appId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new InsertFavoriteFoodCommand(foodId, appId, Language),
            cancellationToken);
        return Ok(new ApiResult<FavoriteFoodGetDto>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [HttpDelete("soft-delete-favorite-food")]
    public async Task<ActionResult> SoftDeleteFavoriteFood(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteFavoriteFoodCommand(id),
            cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success,
            true));
    }
}