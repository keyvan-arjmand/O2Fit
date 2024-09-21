namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class RecipeCategoryController : BaseApiController
{
    private readonly IMediator _mediator;

    public RecipeCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HasPermission(PermissionsConstants.GetByIdRecipeCategory)]
    [HttpGet("get-by-id-recipe-category")]
    public async Task<ActionResult> GetByIdRecipeCategory([FromQuery] GetByIdRecipeCategoryQuery query, CancellationToken cancellationToken)
    {
        var result =  await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<RecipeCategoryDto>(result,string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.GetRecipeCategoryPaginated)]
    [HttpGet("get-recipe-category-paginated")]
    public async Task<ActionResult> GetRecipeCategoryPaginated([FromQuery] GetRecipeCategoryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<PaginationResult<RecipeCategoryDto>>(result,string.Empty, ApiResultStatusCode.Success));
    }
   
    [HasPermission(PermissionsConstants.CreateRecipeCategory)]
    [HttpPost("create-recipe-category")]
    public async Task<ActionResult> CreateRecipeCategory(CreateRecipeCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.UpdateRecipeCategory)]
    [HttpPut("update-recipe-category")]
    public async Task<ActionResult> UpdateRecipeCategory(UpdateRecipeCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.SoftDeleteRecipeCategory)]
    [HttpDelete("soft-delete-recipe-category")]
    public async Task<ActionResult> SoftDeleteRecipeCategory([FromQuery] SoftDeleteRecipeCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}