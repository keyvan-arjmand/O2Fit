using Identity.V2.Application.NutritionistProfiles.V1.Commands.CreateNutritionistProfile;
using Identity.V2.Application.NutritionistProfiles.V1.Commands.UpdateNutritionistProfile;

namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]
public class NutritionistProfileController: BaseApiController
{
    private readonly IMediator _mediator;

    public NutritionistProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HasPermission(PermissionsConstants.CreateNutritionistProfile)]
    [HttpPost("create-nutritionist-profile")]
    public async Task<ActionResult> CreateNutritionistProfile(CreateNutritionistProfileCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.UpdateNutritionistProfile)]
    [HttpPut("update-nutritionist-profile")]
    public async Task<ActionResult> UpdateNutritionistProfile(UpdateNutritionistProfileCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}