using Advertise.Application.NutritionistBannerAdvertises.V1.Commands.DecreaseNutritionistBannerAdvertiseClickCount;
using Advertise.Application.NutritionistBannerAdvertises.V1.Queries.GetNutritionistAdvertiseBannerById;

namespace Advertise.Api.Controllers;

[ApiVersion("1")]
public class NutritionistBannerAdvertiseController : BaseApiController
{
    private readonly IMediator _mediator;

    public NutritionistBannerAdvertiseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HasPermission(PermissionsConstants.GetNutritionistAdvertiseBannerById)]
    [HttpGet("get-nutritionist-advertise-banner-by-id")]
    public async Task<ActionResult> GetNutritionistAdvertiseBannerById(
        [FromQuery] GetNutritionistAdvertiseBannerByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<NutritionistBannerAdvertiseDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNutritionistBannerAdvertise)]
    [HttpPost("create-nutritionist-banner-advertise")]
    public async Task<ActionResult> CreateNutritionistBannerAdvertise(
        [FromBody] CreateNutritionistBannerAdvertiseCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.ActiveNutritionistBannerAdvertise)]
    [HttpPatch("active-nutritionist-banner-advertise")]
    public async Task<ActionResult> ActiveNutritionistBannerAdvertise(
        [FromBody] GetNutritionistBannerAdvertiseIdDto dto,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new ChangeNutritionistBannerAdvertiseStatusCommand(dto.Id, AdvertiseStatus.Active),
            cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.PauseNutritionistBannerAdvertise)]
    [HttpPatch("pause-nutritionist-banner-advertise")]
    public async Task<ActionResult> PauseNutritionistBannerAdvertise([FromBody] GetNutritionistBannerAdvertiseIdDto dto,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new ChangeNutritionistBannerAdvertiseStatusCommand(dto.Id, AdvertiseStatus.Paused),
            cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.IncreaseNutritionistBannerAdvertiseClickCount)]
    [HttpPatch("increase-nutritionist-banner-advertise-click-count")]
    public async Task<ActionResult> IncreaseNutritionistBannerAdvertiseClickCount(
        [FromBody] IncreaseNutritionistBannerAdvertiseClickCountCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.IncreaseNutritionistBannerAdvertiseViewCount)]
    [HttpPatch("increase-nutritionist-banner-advertise-view-count")]
    public async Task<ActionResult> IncreaseNutritionistBannerAdvertiseViewCount(
        [FromBody] IncreaseNutritionistBannerAdvertiseViewCountCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    //not use
    [HasPermission(PermissionsConstants.DecreaseNutritionistBannerAdvertiseClickCount)]
    [HttpPatch("decrease-nutritionist-banner-advertise-click-count")]
    public async Task<ActionResult> DecreaseNutritionistBannerAdvertiseClickCount(
        [FromBody] DecreaseNutritionistBannerAdvertiseClickCountCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}