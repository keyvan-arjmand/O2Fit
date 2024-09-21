using Nutritionist.Application.NutritionistOrders.V1.Commands.AcceptNutritionistOrder;
using Nutritionist.Application.NutritionistOrders.V1.Commands.RejectNutritionistOrder;
using Nutritionist.Application.NutritionistOrders.V1.Queries.GetPendingNutritionistOrdersPaginated;
using Nutritionist.Application.NutritionistUserPackages.V1.Commands.NutritionistAcceptOrder;

namespace Nutritionist.Api.Controllers;

[ApiVersion("1")]
public class NutritionistOrdersController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public NutritionistOrdersController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    //[HasPermission(PermissionsConstants.CreatePermission)]
    //[HttpGet("get-all-nutritionist-orders")]
    //public async Task<ActionResult> GetAllNutritionistOrders([FromQuery] PaginatedDto dto, CancellationToken cancellationToken)
    //{
    //    var language = _currentUserService.Language.ToEnum<Language>();
    //    var result = await _mediator.Send(new GetNutritionistOrdersPaginatedQuery(language,dto.PageNumber,dto.PageSize), cancellationToken);
    //    return Ok(new ApiResult<PaginationResult<NutritionistOrderDto>>(result, string.Empty, ApiResultStatusCode.Success));
    //}

    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpGet("get-all-pending-nutritionist-orders-paginated")]
    public async Task<ActionResult> GetAllNutritionistOrders([FromQuery] PaginatedDto dto,
        CancellationToken cancellationToken)
    {
        var language = _currentUserService.Language.ToEnum<Language>();
        var result =
            await _mediator.Send(new GetPendingNutritionistOrdersPaginatedQuery(language, dto.PageNumber, dto.PageSize),
                cancellationToken);
        return Ok(new ApiResult<PaginationResult<NutritionistOrderDto>>(result, string.Empty,
            ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpPatch("accept-user-order")]
    public async Task<ActionResult> AcceptUserOrder([FromBody] AcceptNutritionistOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }


    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpPatch("reject-user-order")]
    public async Task<ActionResult> RejectUserOrder([FromBody] RejectNutritionistOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}