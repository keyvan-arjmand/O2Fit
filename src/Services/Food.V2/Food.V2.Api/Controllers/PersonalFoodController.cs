using Common.Constants.Food;
using Food.V2.Application.Dtos.PersonalFood;
using Food.V2.Application.PersonalFoods.V1.Commands.InsertPersonalFood;
using Food.V2.Application.PersonalFoods.V1.Commands.UpdatePersonalFood;
using Food.V2.Application.PersonalFoods.V1.Queries.CalculateNutrients;
using Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodById;
using Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodByIds;
using Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodsByUserId;
using Microsoft.AspNetCore.Mvc;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class PersonalFoodController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public PersonalFoodController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-personal-food-by-id")]
    public async Task<ActionResult> Get(string foodId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPersonalFoodByIdQuery(foodId), cancellationToken);
        return Ok(new ApiResult<PersonalFoodDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("get-personal-food-by-food-ids")]
    public async Task<ActionResult> GetByFoodIds(List<string> foodIds,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPersonalFoodByIdsQuery(foodIds), cancellationToken);
        return Ok(new ApiResult<List<PersonalFoodDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPost("insert-personal-food")]
    public async Task<ActionResult> InsertPersonalFood(InsertPersonalFoodCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<PersonalFoodDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPut("update-personal-food")]
    public async Task<ActionResult> UpdatePersonalFood(UpdatePersonalFoodCommand request, /*IFormFile image,*/
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult<PersonalFoodDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpPut("get-personal-measureUnits")]
    public async Task<ApiResult<List<string>>> GetPersonalMeasureUnits(CancellationToken cancellationToken)
    {
        return new ApiResult<List<string>>(new List<string>()
        {
            Keys.Grams,
            Keys.Pounds,
            Keys.Ounces,
        }, string.Empty, ApiResultStatusCode.Success);
    }

    [HttpPost("calculate-nutrients")]
    public async Task<ApiResult<List<decimal>>> CalculateNutrients(CalculateNutrientsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return new ApiResult<List<decimal>>(result, string.Empty, ApiResultStatusCode.Success);
    }

    [HttpGet("get-by-user-id")]
    public virtual async Task<List<PersonalFoodDto>> GetByUserId(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPersonalFoodsByUserIdQuery { UserId = _currentUserService.UserId! },
            cancellationToken);
    }
}