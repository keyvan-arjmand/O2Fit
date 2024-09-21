using Food.V2.Application.Dtos.MissingFood;
using Food.V2.Application.Dtos.ProblemFood;
using Food.V2.Application.MissingFoods.V1.Commands.InsertReportMissingFood;
using Food.V2.Application.MissingFoods.V1.Commands.SoftDeleteMissingFood;
using Food.V2.Application.MissingFoods.V1.Queries.MissingFoodGetAllPaging;
using Food.V2.Application.ProblemFoods.V1.Commands.InsertReportMissingFood;
using Food.V2.Application.ProblemFoods.V1.Commands.SoftDeleteMissingFood;
using Food.V2.Application.ProblemFoods.V1.Queries.FaultFoodGetAllPaging;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class ReportProblemFoodsController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public ReportProblemFoodsController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-all-paging-report")]
    public async Task<IActionResult> MissingFoodGetAllPaging(int page, int pageSize
        , CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new FaultFoodGetAllPagingQuery(page, pageSize), cancellationToken);
        return Ok(new ApiResult<PaginationResult<FaultFoodDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPost("insert-report")]
    public async Task<IActionResult> Post(InsertReportProblemFoodCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpDelete("soft-delete-report")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteProblemFoodCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}