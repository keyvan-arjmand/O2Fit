using Identity.V2.Application.Dtos.SpecialDiseases;
using Identity.V2.Application.SpecialDiseases.V1.Commands.CreateSpecialDisease;
using Identity.V2.Application.SpecialDiseases.V1.Commands.SoftDeleteSpecialDisease;
using Identity.V2.Application.SpecialDiseases.V1.Commands.UpdateSpecialDisease;
using Identity.V2.Application.SpecialDiseases.V1.Queries.GetAllSpecialDiseases;

namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]
public class SpecialDiseaseController : BaseApiController
{
    private readonly IMediator _mediator;

    public SpecialDiseaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HasPermission(PermissionsConstants.GetAllSpecialDiseases)]
    [HttpGet("get-all-special-diseases")]
    public async Task<ActionResult> GetAllSpecialDiseases(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSpecialDiseasesQuery(), cancellationToken);
        return Ok(new ApiResult<List<SpecialDiseaseDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.GetSpecialDiseaseById)]
    [HttpGet("get-special-disease-by-id")]
    public async Task<ActionResult> GetSpecialDiseaseById(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSpecialDiseasesQuery(), cancellationToken);
        return Ok(new ApiResult<List<SpecialDiseaseDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateSpecialDisease)]
    [HttpPost("create-special-disease")]
    public async Task<ActionResult> CreateSpecialDisease([FromBody] CreateSpecialDiseaseCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.UpdateSpecialDisease)]
    [HttpPut("update-special-disease")]
    public async Task<ActionResult> UpdateSpecialDisease([FromBody] UpdateSpecialDiseaseCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.SoftDeleteSpecialDiseaseById)]
    [HttpDelete("soft-delete-special-disease-by-id")]
    public async Task<ActionResult> SoftDeleteSpecialDiseaseById([FromQuery] string id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteSpecialDiseaseCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}