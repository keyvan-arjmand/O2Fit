namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]
public class CategoryPermissionController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IRoleService _roleService;

    public CategoryPermissionController(IMediator mediator, IRoleService roleService)
    {
        _mediator = mediator;
        _roleService = roleService;
    }
    
    [HasPermission(PermissionsConstants.GetPermissionCategoriesAllPaginated)]
    //[EnableRateLimiting(O2fitIdentityConstants.RateLimitPolicyName)]
    [HttpGet("get-all-paginated")]
    public async Task<ActionResult> GetAllPaginated(
        [FromQuery] GetAllPermissionCategoriesPaginatedQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult<PaginationResult<CategoryPermissionPaginatedDto>>(result, string.Empty,
            ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.GetCategoryPermissionByIdWithPermissions)]
    [HttpGet("get-by-id-with-permissions")]
    public async Task<ActionResult> GetByIdWithPermissions(
        [FromQuery] GetPermissionCategoryByIdWithPermissionQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult<CategoryPermissionWithPermissionsDto>(result, string.Empty,
            ApiResultStatusCode.Success));
    }
    /// <summary>
    /// first select role then select permissions
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HasPermission(PermissionsConstants.GetAllCategoryPermissionsWithPermissions)]
    [HttpGet("get-all-category-permissions-with-permissions")]
    public async Task<ActionResult> GetAllCategoryPermissionsWithPermissions(string roleId ,CancellationToken cancellationToken)
    {

        var role = await _roleService.GetByIdAsync(roleId).ConfigureAwait(false);

        if (role is null)
            return NotFound(new ApiResult<List<CategoryPermissionWithPermissionsForTreeViewDto>?>(null, "Role not found",
                ApiResultStatusCode.NotFound, false));

        if(!role.Permissions.Any())
            return BadRequest(new ApiResult<List<CategoryPermissionWithPermissionsForTreeViewDto>?>(null, "Role don't have permission field",
                ApiResultStatusCode.BadRequest,false));
        
        var data = await _mediator.Send(new GetAllCategoryPermissionsWithPermissionsQuery()
            , cancellationToken).ConfigureAwait(false);

        foreach (var categoryPermission in data)      
        {
            foreach (var permission in categoryPermission.Permissions)
            {
                if (role.Permissions.Any(x=>x.Name == permission.Name))
                {
                    permission.IsSelected = true;
                }
            }
        }
        return Ok(new ApiResult<List<CategoryPermissionWithPermissionsForTreeViewDto>?>(data, string.Empty,
            ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.CreateCategoryPermission)]
    [HttpPost("create-category-permission")]
    public async Task<ActionResult> CreateCategoryPermission([FromBody] CreateCategoryPermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult<string>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateCategoryPermission)]
    [HttpPut("update-category-permission")]
    public async Task<ActionResult> UpdateCategoryPermission([FromBody] UpdateCategoryPermissionCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

        
    [HasPermission(PermissionsConstants.DeleteCategoryPermission)]
    [HttpDelete("delete-category-permission")]
    public async Task<ActionResult> DeleteCategoryPermission([FromBody] DeleteCategoryPermissionByIdCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}