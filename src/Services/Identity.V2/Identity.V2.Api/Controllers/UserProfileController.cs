using Identity.V2.Application.UserProfiles.V1.Commands.PartialUpdateUserProfile;
using Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfile;
using Identity.V2.Application.UserProfiles.V1.Commands.UpdateUserProfileTargetNutrient;
using Identity.V2.Application.Users.V1.Commands.ChangeIsCompleteValue;
using Identity.V2.Domain.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Identity.V2.Api.Controllers;

[ApiVersion("1")]

public class UserProfileController :BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;
    public UserProfileController(IMediator mediator, IUserService userService)
    {
        _mediator = mediator;
        _userService = userService;
    }
    
    [HasPermission(PermissionsConstants.GetUserNutrient)]
    [HttpGet("get-user-nutrient")]
    public async Task<ActionResult> GetUserNutrient(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.BadRequest));

        return Ok(new ApiResult<List<NotNegativeForDoubleTypes>>(user.UserProfile.Target.TargetNutrient, string.Empty,
            ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.CreateUserProfile)]
    [HttpPatch("update-user-profile")]
    public async Task<ActionResult> UpdateUserProfile([FromBody] UpdateUserProfileCommand command,
        CancellationToken cancellationToken)
    {
        var profileId = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

   
    [HasPermission(PermissionsConstants.UpdateFastingMode)]
    [HttpPatch("update-fasting-mode")]
    public async Task<ActionResult> UpdateFastingMode([FromBody] UpdateFastingModeCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateTarget)]
    [HttpPatch("update-target")]
    public async Task<ActionResult> UpdateTarget([FromBody] UpdateUserProfileTargetCommand command,
        CancellationToken cancellationToken)
    {
        command.TargetStep = (command.TargetStep < 1) ? 5000 : command.TargetStep;
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateTargetNutrient)]
    [HttpPatch("update-target-nutrient")]
    public async Task<ActionResult> UpdateTargetNutrient([FromBody] UpdateUserProfileTargetNutrientCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.UpdateProfile)]
    [HttpPatch("update-profile")]
    public async Task<ActionResult> UpdateProfile([FromBody] PartialUpdateUserProfileCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    [HasPermission(PermissionsConstants.ChangeIsComplete)]
    [HttpPatch("change-is-complete")]
    public async Task<ActionResult> ChangeIsComplete([FromBody] ChangeIsCompleteValueCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}