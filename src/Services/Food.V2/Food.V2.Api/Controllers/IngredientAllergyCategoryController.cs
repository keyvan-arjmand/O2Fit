using Food.V2.Application.IngredientAllergyCategories.V1.Commands.AddChildrenToIngredientAllergyCategory;
using Food.V2.Application.IngredientAllergyCategories.V1.Commands.CreateRootIngredientAllergy;
using Food.V2.Application.IngredientAllergyCategories.V1.Commands.DeleteIngredientAllergy;
using Food.V2.Application.IngredientAllergyCategories.V1.Commands.RemoveChildFromIngredientAllergyCategory;

namespace Food.V2.Api.Controllers;

public class IngredientAllergyCategoryController : BaseApiController
{
    private readonly IMediator _mediator;

    public IngredientAllergyCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HasPermission(PermissionsConstants.CreateRootIngredientAllergy)]
    [HttpPost("create-root-ingredient-allergy")]
    public async Task<ActionResult> CreateRootIngredientAllergy([FromBody]CreateRootIngredientAllergyCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.AddChildrenToIngredientAllergyCategory)]
    [HttpPatch("add-children-to-ingredient-allergy-category")]
    public async Task<ActionResult> AddChildrenToIngredientAllergyCategory(
        [FromBody]AddChildrenToIngredientAllergyCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.DeleteIngredientAllergy)]
    [HttpDelete("delete-ingredient-allergy")]
    public async Task<ActionResult> DeleteIngredientAllergy([FromQuery] DeleteIngredientAllergyCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.RemoveChildFromIngredientAllergyCategory)]
    [HttpDelete("remove-child-from-ingredient-allergy-category")]
    public async Task<ActionResult> RemoveChildFromIngredientAllergyCategory(
        [FromQuery]RemoveChildFromIngredientAllergyCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
}