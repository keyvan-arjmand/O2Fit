namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class MeasureUnitController : BaseApiController
{
    private readonly IMediator _mediator;

    public MeasureUnitController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HasPermission(PermissionsConstants.GetAllMeasureUnits)]
    [HttpGet("get-all-measure-units")]
    public async Task<ActionResult> GetAllMeasureUnits(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllMeasureUnitsQuery(), cancellationToken);
        return Ok(new ApiResult<List<MeasureUnitDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.GetMeasureUnitById)]
    [HttpGet("get-measure-unit-by-id")]
    public async Task<ActionResult> GetMeasureUnitById(string id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMeasureUnitByIdQuery(id), cancellationToken);
        return Ok(new ApiResult<MeasureUnitDto>(result, string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.CreateMeasureUnit)]
    [HttpPost("create-measure-unit")]
    public async Task<ActionResult> CreateMeasureUnit([FromBody] CreateMeasureUnitCommand command,
        CancellationToken cancellationToken)
    {
        var id =  await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult<string>(id, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateMeasureUnit)]
    [HttpPut("update-measure-unit")]
    public async Task<ActionResult> UpdateMeasureUnit([FromBody] UpdateMeasureUnitCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}