using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.FoodCategories.V1.Commands.CreateCategory;
using Food.V2.Application.FoodCategories.V1.Commands.SofDeleteCategory;
using Food.V2.Application.FoodCategories.V1.Commands.UpdateCategory;
using Food.V2.Application.FoodCategories.V1.Queries.GetAllCategory;
using Food.V2.Application.FoodCategories.V1.Queries.GetAllCategoryPagination;
using Food.V2.Application.FoodCategories.V1.Queries.GetByParentIdCategory;
using Food.V2.Application.FoodCategories.V1.Queries.GetCategoryById;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class FoodCategoryController : BaseApiController
{
    private readonly IMediator _mediator;

    public FoodCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("get-all-category-pagination")]
    [HasPermission(PermissionsConstants.GetAllCategoryPagination)]
    public async Task<IActionResult> GetAllCategoryPagination(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCategoryPaginationQuery(page, pageSize), cancellationToken);
        return Ok(new ApiResult<List<FoodCategoryDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    [HttpGet("get-by-id")]
    [HasPermission(PermissionsConstants.GetByIdFoodCategory)]
    public async Task<ActionResult> GetByIdFoodCategory(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
        return Ok(new ApiResult<FoodCategoryDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HttpGet("get-by-parent-id")]
    [HasPermission(PermissionsConstants.GetByParentIdFoodCategory)]
    public async Task<ActionResult> GetByParentIdFoodCategory(string parentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByParentIdCategoryQuery(parentId), cancellationToken);
        return Ok(new ApiResult<List<FoodCategoryDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HttpGet("get-all")]
    [HasPermission(PermissionsConstants.GetAllFoodCategory)]
    public async Task<ActionResult> GetAllFoodCategory(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllCategoryQuery(), cancellationToken);
        return Ok(new ApiResult<List<FoodCategoryDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    [HttpPost("category")]
    [HasPermission(PermissionsConstants.CreateFoodCategory)]
    public async Task<ApiResult> CreateFoodCategory(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }

    [HttpPut("category")]
    [HasPermission(PermissionsConstants.PatchFoodCategory)]
    public async Task<ApiResult> PatchFoodCategory(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
    [HttpDelete("soft-delete-category")]
    [HasPermission(PermissionsConstants.SoftDeleteFoodCategory)]
    public async Task<ApiResult> SoftDeleteFoodCategory(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SofDeleteCategoryCommand(id), cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success);
    }
}