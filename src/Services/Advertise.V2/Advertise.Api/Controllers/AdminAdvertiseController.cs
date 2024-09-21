using Advertise.Application.AdminAdvertises.V1.Commands.IncreaseClickCountAndSubtractCostFromBudget;
using Advertise.Application.AdminAdvertises.V1.Commands.IncreaseViewCount;

namespace Advertise.Api.Controllers;

[ApiVersion("1")]
public class AdminAdvertiseController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    
    public AdminAdvertiseController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HasPermission(PermissionsConstants.GetAllActiveAdminAdvertises)]
    [HttpGet("get-all-active-admin-advertises")]
    public async Task<ActionResult> GetAllActiveAdminAdvertises(CancellationToken cancellationToken)
    {
        var language = _currentUserService.Language.ToEnum<Language>();
        var result = await _mediator.Send(new GetAllActiveAdminAdvertiseQuery(language), cancellationToken);
        return Ok(new ApiResult<List<AdminAdvertiseDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.CreateAdminAdvertise)]
    [HttpPost("create-admin-advertise")]
    public async Task<ActionResult> CreateAdminAdvertise([FromBody]CreateAdminAdvertiseCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.PauseAdminAdvertise)]
    [HttpPatch("pause-admin-advertise")]
    public async Task<ActionResult> PauseAdminAdvertise([FromBody] GetAdminAdvertiseIdDto dto,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new ChangeAdminAdvertiseStatusCommand(dto.Id, AdvertiseStatus.Paused), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.ActiveAdminAdvertise)]
    [HttpPatch("active-admin-advertise")]
    public async Task<ActionResult> ActiveAdminAdvertise([FromBody] GetAdminAdvertiseIdDto dto,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new ChangeAdminAdvertiseStatusCommand(dto.Id, AdvertiseStatus.Active), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    //not use
    [HasPermission(PermissionsConstants.DecreaseViewCount)]
    [HttpPatch("decrease-view-count")]
    public async Task<ActionResult> DecreaseViewCount([FromBody] DecreaseViewCountCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.IncreaseClickCount)]
    [HttpPatch("increase-click-count-and-subtract-cost-from-budget")]
    public async Task<ActionResult> IncreaseClickCountAndSubtractCostFromBudget([FromBody] IncreaseClickCountAndSubtractCostFromBudgetCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.IncreaseViewCount)]
    [HttpPatch("increase-view-count")]
    public async Task<ActionResult> IncreaseViewCount([FromBody] IncreaseViewCountCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.SoftDeleteAdminAdvertise)]
    [HttpDelete("soft-delete-admin-advertise")]
    public async Task<ActionResult> SoftDeleteAdminAdvertise([FromQuery] SoftDeleteAdminAdvertiseCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
}