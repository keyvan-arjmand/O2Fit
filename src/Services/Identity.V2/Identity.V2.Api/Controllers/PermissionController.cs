namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]
public class PermissionController : BaseApiController
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpPost("create-permission")]
    public async Task<ActionResult> CreatePermission([FromBody]CreatePermissionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult<string>(result, string.Empty, ApiResultStatusCode.Success));
    }
        
    [HasPermission(PermissionsConstants.UpdatePermission)]
    [HttpPut("update-permission")]
    public async Task<ActionResult> UpdatePermission([FromBody] UpdatePermissionCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
        
    [HasPermission(PermissionsConstants.DeletePermission)]
    [HttpDelete("delete-permission")]
    public async Task<ActionResult> DeletePermission([FromBody] DeletePermissionCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}