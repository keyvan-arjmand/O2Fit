using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.Nationalities.V1.Commands.CreateNationality;
using Food.V2.Application.Nationalities.V1.Commands.SoftDeleteNationality;
using Food.V2.Application.Nationalities.V1.Commands.UpdateNationality;
using Food.V2.Application.Nationalities.V1.Queries.GetAllNationality;
using Food.V2.Application.Nationalities.V1.Queries.GetAllNationalityPagination;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class NationalityController : BaseApiController
{
    private readonly IMediator _mediator;

    public NationalityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-all-nationality-pagination")]
    [HasPermission(PermissionsConstants.GetAllNationalityPagination)]
    public async Task<IActionResult> GetAllNationalityPagination(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllNationalityPaginationQuery(page, pageSize), cancellationToken);
        return Ok(new ApiResult<List<NationalityDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.GetAllNationality)]
    [HttpGet("get-all-nationality")]
    public async Task<ActionResult> GetAllNationality(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllNationalityQuery(), cancellationToken);
        return Ok(new ApiResult<List<NationalityDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPost("nationality")]
    public async Task<ActionResult> CreateNationality(CreateNationalityCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.PatchNationality)]
    [HttpPut("nationality")]
    public async Task<ActionResult> PatchNationality(UpdateNationalityCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.DeleteNationality)]
    [HttpDelete("soft-delete-nationality")]
    public async Task<ActionResult> DeleteNationality(SoftDeleteNationalityCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}