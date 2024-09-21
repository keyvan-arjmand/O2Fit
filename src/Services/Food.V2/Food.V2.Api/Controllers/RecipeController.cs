using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Recipes.V1.Commands.ChangeRecipeStatus;
using Food.V2.Application.Recipes.V1.Commands.SoftDeleteRecipe;
using Food.V2.Application.Recipes.V1.Queries.GetAllPagingRecipe;
using Food.V2.Application.Recipes.V1.Queries.GetFullRecipeById;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class RecipeController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public RecipeController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-all-paging-recipe")]
    public async Task<IActionResult> GetAllPaging(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllPagingRecipeQuery(page, pageSize), cancellationToken);
        return Ok(new ApiResult<PaginationResult<RecipeDto>>(result,string.Empty, ApiResultStatusCode.Success));

    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-recipe-by-id")]
    public async Task<FullRecipeDto> GetFullRecipeById(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFullRecipeByIdQuery(id), cancellationToken);
    }


    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPatch("change-recipe-status")]
    public async Task<IActionResult> ChangeStatus(ChangeRecipeStatusCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpDelete("soft-delete-recipe")]
    public async Task<IActionResult> SoftDelete(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteRecipeCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
}