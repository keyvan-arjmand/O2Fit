using Workout.V2.Application.Common.Constants;
using Workout.V2.Application.Workouts.V1.Commands.CreateBodyBuilding;
using Workout.V2.Application.Workouts.V1.Commands.CreateCardio;
using Workout.V2.Application.Workouts.V1.Commands.EditBodyBuilding;
using Workout.V2.Application.Workouts.V1.Commands.EditCardio;
using Workout.V2.Application.Workouts.V1.Commands.SoftDeleteBodyBuilding;
using Workout.V2.Application.Workouts.V1.Commands.SoftDeleteCardio;
using Workout.V2.Application.Workouts.V1.Queries.GetBodyBuildingById;
using Workout.V2.Application.Workouts.V1.Queries.GetBodyBuildingPaginated;
using Workout.V2.Application.Workouts.V1.Queries.GetCardioById;
using Workout.V2.Application.Workouts.V1.Queries.GetCardioPaginated;

namespace Workout.V2.Api.Controllers;

[ApiVersion("1")]
public class WorkoutController : BaseApiController
{
    private readonly IMediator _mediator;

    public WorkoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //redis
    [HasPermission(PermissionsConstants.GetExercisePaginated)]
    [HttpGet("get-exercise-paginated")]
    public async Task<ActionResult> GetExercisePaginated([FromQuery] GetExercisePaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<PaginationResult<ExerciseDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    //redis
    [HasPermission(PermissionsConstants.GetExerciseById)]
    [HttpGet("get-exercise-by-id")]
    public async Task<ActionResult> GetExerciseById([FromQuery] GetExerciseByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<ExerciseDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    //redis
    [HasPermission(PermissionsConstants.GetCardioPaginated)]
    [HttpGet("get-cardio-paginated")]
    public async Task<ActionResult> GetCardioPaginated([FromQuery] GetCardioPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<PaginationResult<CardioDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    //redis
    [HasPermission(PermissionsConstants.GetCardioById)]
    [HttpGet("get-cardio-by-id")]
    public async Task<ActionResult> GetCardioById([FromQuery] GetCardioByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<CardioDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    //redis
    [HasPermission(PermissionsConstants.GetBodyBuildingPaginated)]
    [HttpGet("get-body-building-paginated")]
    public async Task<ActionResult> GetBodyBuildingPaginated([FromQuery] GetBodyBuildingPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<PaginationResult<BodyBuildingDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    //redis
    [HasPermission(PermissionsConstants.GetBodyBuildingById)]
    [HttpGet("get-body-building-by-id")]
    public async Task<ActionResult> GetBodyBuildingById([FromQuery] GetBodyBuildingByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<BodyBuildingDto>(result, string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.CreateExercise)]
    [HttpPost("create-exercise")]
    public async Task<ActionResult> CreateExercise([FromBody] CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateCardio)]
    [HttpPost("create-cardio")]
    public async Task<ActionResult> CreateCardio([FromForm] CreateCardioCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateBodyBuilding)]
    [HttpPost("create-body-building")]
    public async Task<ActionResult> CreateBodyBuilding([FromForm] CreateBodyBuildingCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.EditExercise)]
    [HttpPatch("edit-exercise")]
    public async Task<ActionResult> EditExercise([FromBody] EditExerciseCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.EditCardio)]
    [HttpPatch("edit-cardio")]
    public async Task<ActionResult> EditCardio([FromBody] EditCardioCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.EditBodyBuilding)]
    [HttpPatch("edit-body-building")]
    public async Task<ActionResult> EditBodyBuilding([FromBody] EditBodyBuildingCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.SoftDeleteExercise)]
    [HttpDelete("soft-delete-exercise")]
    public async Task<ActionResult> SoftDeleteExercise([FromBody] SoftDeleteExerciseCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }


    [HasPermission(PermissionsConstants.SoftDeleteCardio)]
    [HttpDelete("soft-delete-cardio")]
    public async Task<ActionResult> SoftDeleteCardio([FromBody] SoftDeleteCardioCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.SoftDeleteBodyBuilding)]
    [HttpDelete("soft-delete-body-building")]
    public async Task<ActionResult> SoftDeleteBodyBuilding([FromBody] SoftDeleteBodyBuildingCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
}