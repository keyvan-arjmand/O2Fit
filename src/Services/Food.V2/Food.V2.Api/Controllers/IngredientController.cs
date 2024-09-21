using Food.V2.Application.Ingredients.V1.Commands.CreateIngredient;
using Food.V2.Application.Ingredients.V1.Commands.DeleteIngredientById;
using Food.V2.Application.Ingredients.V1.Commands.UpdateIngredient;
using Food.V2.Application.Ingredients.V1.Queries.GetByIdAdminIngredient;
using Food.V2.Application.Ingredients.V1.Queries.GetIngredientById;
using Food.V2.Application.Ingredients.V1.Queries.SearchIngredientByNamePaginated;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class IngredientController : BaseApiController
{
    private readonly IMediator _mediator;

    public IngredientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HasPermission(PermissionsConstants.GetByIdAdminIngredient)]
    [HttpGet("get-by-id-admin-ingredient")]
    public async Task<ActionResult> GetByIdAdminIngredient([FromQuery] GetByIdAdminIngredientQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<GetByIdAdminIngredientDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.GetIngredientById)]
    [HttpGet("get-ingredient-by-id")]
    public async Task<ActionResult> GetIngredientById([FromQuery] GetIngredientByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<GetIngredientByIdDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    //redis
    [HasPermission(PermissionsConstants.SearchIngredientByNamePaginated)]
    [HttpGet("search-ingredient-by-name-paginated")]
    public async Task<ActionResult> SearchIngredientByNamePaginated([FromQuery] SearchIngredientByNamePaginated dto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SearchIngredientByNamePaginatedQuery(dto.Name, dto.PageIndex, dto.PageSize, LanguageName), cancellationToken);
        return Ok(new ApiResult<PaginationResult<SearchIngredientByNameDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.CreateIngredient)]
    [HttpPost("create-ingredient")]
    public async Task<ActionResult> CreateIngredient(CreateIngredientCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.UpdateIngredient)]
    [HttpPut("update-ingredient")]
    public async Task<ActionResult> UpdateIngredient(UpdateIngredientCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.DeleteIngredientById)]
    [HttpDelete("delete-ingredient-by-id")]
    public async Task<ActionResult> DeleteIngredientById(DeleteIngredientByIdCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}