using Workout.V2.Application.BodyMuscles.V1.Commands.CreateBodyMuscle;
using Workout.V2.Application.BodyMuscles.V1.Commands.DeleteBodyMuscle;
using Workout.V2.Application.BodyMuscles.V1.Commands.EditBodyMuscle;
using Workout.V2.Application.BodyMuscles.V1.Commands.SoftDeleteBodyMuscle;
using Workout.V2.Application.BodyMuscles.V1.Queries.GetAllBodyMuscle;
using Workout.V2.Application.BodyMuscles.V1.Queries.GetBodyMuscleById;
using Workout.V2.Application.Dtos.BodyMuscles;

namespace Workout.V2.Api.Controllers;

[ApiVersion("1")]
public class BodyMuscleController : BaseApiController
{
    private readonly IMediator _mediator;

    public BodyMuscleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //redis
    [HasPermission(PermissionsConstants.GetAllBodyMuscle)]
    [HttpGet("get-all-body-muscle")]
    public async Task<ActionResult> GetAllBodyMuscle(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllBodyMuscleQuery(), cancellationToken);
        return Ok(new ApiResult<List<BodyMuscleDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    //redis
    [HasPermission(PermissionsConstants.GetBodyMuscleById)]
    [HttpGet("get-body-muscle-by-id")]
    public async Task<ActionResult> GetBodyMuscleById([FromQuery]  string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBodyMuscleByIdQuery(id), cancellationToken);
        return Ok(new ApiResult<BodyMuscleDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateBodyMuscle)]
    [HttpPost("create-body-muscle")]
    public async Task<ActionResult> CreateBodyMuscle([FromBody] CreateBodyMuscleCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateBodyMuscle)]
    [HttpPut("update-body-muscle")]
    public async Task<ActionResult> UpdateBodyMuscle([FromBody] EditBodyMuscleCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.SoftDeleteBodyMuscle)]
    [HttpDelete("soft-delete-body-muscle")]
    public async Task<ActionResult> SoftDeleteBodyMuscle([FromQuery] string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteBodyMuscleCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.DeleteBodyMuscle)]
    [HttpDelete("delete-body-muscle")]
    public async Task<ActionResult> DeleteBodyMuscle([FromQuery] string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBodyMuscleCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}