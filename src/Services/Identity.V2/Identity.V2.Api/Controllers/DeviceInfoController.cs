using Identity.V2.Application.DeviceInfo.V1.Commands.AddDeviceInfo;

namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]
public class DeviceInfoController : BaseApiController
{
    private readonly IMediator _mediator;

    public DeviceInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HasPermission(PermissionsConstants.AddDeviceInfo)]
    [HttpPost("add-device-info")]
    public async Task<ActionResult> AddDeviceInfo([FromBody] AddDeviceInfoCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
}