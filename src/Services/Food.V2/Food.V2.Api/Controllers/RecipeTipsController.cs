using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Application.RecipeSteps.V1.Queries.GetByIdRecipeStep;
using Food.V2.Application.RecipeTips.V1.Commands.CreateRecipeTips;
using Food.V2.Application.RecipeTips.V1.Commands.PatchRecipeTip;
using Food.V2.Application.RecipeTips.V1.Commands.SoftDeleteRecipeTip;
using Food.V2.Application.RecipeTips.V1.Queries.GetAllRecipeTips;
using Food.V2.Application.RecipeTips.V1.Queries.GetByIdRecipeTip;
using Food.V2.Application.RecipeTips.V1.Queries.GetRecipeTipsByFoodId;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class RecipeTipsController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public RecipeTipsController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-all-recipe-tip")]
    public async Task<List<RecipeTipDto>> GetAllRecipeStep(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllRecipeTipsQuery(), cancellationToken);
    }

    // [HasPermission(PermissionsConstants.CreateNationality)]
    // [HttpGet("get-by-id-recipe-tip")]
    // public async Task<RecipeTipDto> GetByIdRecipe(string id, string foodId, CancellationToken cancellationToken)
    // {
    //     return await _mediator.Send(new GetByIdRecipeTipQuery(id, foodId), cancellationToken);
    // }

    [HttpGet("get-recipe-tip-by-food-id")]
    [HasPermission(PermissionsConstants.CreateNationality)]
    public async Task<List<TranslationDto>> GetRecipeStepsByFoodId(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetRecipeTipsByFoodIdQuery(id), cancellationToken);
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpDelete("soft-delete-tip-step")]
    public async Task<IActionResult> SoftDeleteRecipeStep(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteRecipeTipCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPost("recipe-tip")]
    public async Task<IActionResult> CreateRecipeSteps(CreateRecipeTipsCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPatch("patch-tip")]
    public async Task<IActionResult> PatchStep(PatchRecipeTipCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}