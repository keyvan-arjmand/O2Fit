using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Application.RecipeSteps.V1.Commands.CreateRecipeSteps;
using Food.V2.Application.RecipeSteps.V1.Commands.PatchRecipeStep;
using Food.V2.Application.RecipeSteps.V1.Commands.SoftDeleteRecipeStep;
using Food.V2.Application.RecipeSteps.V1.Queries.GetAllRecipeSteps;
using Food.V2.Application.RecipeSteps.V1.Queries.GetByIdRecipeStep;
using Food.V2.Application.RecipeSteps.V1.Queries.GetRecipeStepsByFoodId;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class RecipeStepController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public RecipeStepController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-all-recipe-step")]
    public async Task<List<ListRecipeStepDto>> GetAllRecipeStep(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllRecipeStepsQuery(), cancellationToken);
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-by-id-recipe-step")]
    public async Task<RecipeStepDto> GetByIdRecipe(string id, string foodId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetByIdRecipeQuery(id, foodId), cancellationToken);
    }

    [HttpGet("get-recipe-steps-by-food-id")]
    [HasPermission(PermissionsConstants.CreateNationality)]
    public async Task<List<TranslationDto>> GetRecipeStepsByFoodId(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetRecipeStepsByFoodIdQuery(id), cancellationToken);
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpDelete("soft-delete-recipe-step")]
    public async Task<IActionResult> SoftDeleteRecipeStep(string id, string foodId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteRecipeStepCommand(id, foodId), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPost("recipe-steps")]
    public async Task<IActionResult> CreateRecipeSteps(CreateRecipeStepsCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPatch("patch-steps")]
    public async Task<IActionResult> PatchStep(PatchRecipeStepCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}