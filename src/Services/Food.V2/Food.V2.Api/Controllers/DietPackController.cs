namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class DietPackController : BaseApiController
{
    private readonly IMediator _mediator;

    public DietPackController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HasPermission(PermissionsConstants.GetDietPackById)]
    [HttpGet("get-diet-pack-by-id")]
    public async Task<ActionResult> GetDietPackById([FromQuery] string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDietPackByIdQuery(id), cancellationToken);
        if (result.DietPackFoods.Count > 0)
        {
            foreach (var dietPackFood in result.DietPackFoods)
            {
                var foodTranslation =
                    await _mediator.Send(new GetFoodNameByIdQuery(dietPackFood.FoodId), cancellationToken);
                dietPackFood.FoodName = foodTranslation.Persian;

                var measureUnit = await _mediator.Send(new GetMeasureUnitByIdQuery(dietPackFood.MeasureUnitId),
                    cancellationToken);
                dietPackFood.MeasureUnitName = measureUnit.Name.Persian;
                dietPackFood.MeasureUnitValue = measureUnit.Value;
            }

            
        }
        return Ok(new ApiResult<DietPackDto>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.GetAllPaginated)]
    [HttpGet("get-all-paginated")]
    public async Task<ActionResult> GetAllPaginated([FromQuery] GetAllDietPackPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(new ApiResult<PaginationResult<GetAllDietPackDto>>(result, string.Empty,
            ApiResultStatusCode.Success));
    }

    [Cached(0.2)]
    [HasPermission(PermissionsConstants.GetUserPackage)]
    [HttpGet("get-user-package")]
    public async Task<ActionResult> GetUserPackage([FromQuery] GetUserPackageQuery query,
        CancellationToken cancellationToken)
    {
        var result =  await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    
    [HasPermission(PermissionsConstants.CreateDietPack)]
    [HttpPost("create-diet-pack")]
    public async Task<ActionResult> CreateDietPack(CreateDietPackDto dto, CancellationToken cancellationToken)
    {
        var persian = string.Empty;
        var arabic = string.Empty;
        var english = string.Empty;
        var allergyCategoriesListIds = new List<string>();

        foreach (var dietPackFood in dto.DietPackFoods)
        {
            var foodTranslation =
                await _mediator.Send(new GetFoodNameByIdQuery(dietPackFood.FoodId), cancellationToken);
            persian += foodTranslation.Persian + " - ";
            arabic += foodTranslation.Arabic + " - ";
            english += foodTranslation.English + " - ";

            dietPackFood.FoodName = foodTranslation.Persian;
            
            var ingredientIds =
                await _mediator.Send(new GetFoodIngredientIdsQuery(dietPackFood.FoodId), cancellationToken);

            foreach (var ingredientId in ingredientIds)
            {
                var allergy = await _mediator.Send(new GetIngredientAllergyIdsQuery(ingredientId),
                    cancellationToken);
                if(!string.IsNullOrEmpty(allergy))
                    allergyCategoriesListIds.Add(allergy);
            }

            allergyCategoriesListIds = allergyCategoriesListIds.Distinct().ToList();

            foreach (var nationalityId in dto.NationalityIds)
            {
                var isNationalityExits =
                    await _mediator.Send(new IsNationalityExitsQuery(nationalityId), cancellationToken);
                if (!isNationalityExits)
                    throw new BadRequestException("NationalityId not exits");
            }

            foreach (var dietCategoryId in dto.DietCategoryIds)
            {
                var dietCategoryIdExits =
                    await _mediator.Send(new IsDietCategoryExitsQuery(dietCategoryId), cancellationToken);
                if (!dietCategoryIdExits)
                    throw new BadRequestException("DietCategoryId not exits");
            }

            var measureUnit = await _mediator.Send(new GetMeasureUnitByIdQuery(dietPackFood.MeasureUnitId),cancellationToken);
            dietPackFood.MeasureUnitName = measureUnit.Name.Persian;
            dietPackFood.MeasureUnitValue = measureUnit.Value;
        }

        await _mediator.Send(new CreateDietPackCommand(new CreateDietPackTranslationDto
            {
                Persian = persian,
                Arabic = arabic,
                English = english
            }, dto.FoodMeal, dto.SpecialDiseases, dto.CalorieValue, dto.NutrientValues, dto.IsActive,
            dto.DietCategoryIds, dto.DailyCalorie, dto.ParentCategoryId, dto.NationalityIds,
            dto.DietPackFoods, allergyCategoriesListIds), cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }
    
    [HasPermission(PermissionsConstants.UpdateDietPack)]
    [HttpPut("update-diet-pack")]
    public async Task<ActionResult> UpdateDietPack(UpdateDietPackDto dto, CancellationToken cancellationToken)
    {
        var isExists = await _mediator.Send(new IsDietPackExitsByIdQuery(dto.Id), cancellationToken);
        if (!isExists)
            throw new NotFoundException(nameof(DietPack), dto.Id);
        
        var persian = string.Empty;
        var arabic = string.Empty;
        var english = string.Empty;
        var allergyCategoriesListIds = new List<string>();

        foreach (var dietPackFood in dto.DietPackFoods)
        {
            var foodTranslation =
                await _mediator.Send(new GetFoodNameByIdQuery(dietPackFood.FoodId), cancellationToken);
            persian += foodTranslation.Persian + " - ";
            arabic += foodTranslation.Arabic + " - ";
            english += foodTranslation.English + " - ";
            
            dietPackFood.FoodName = foodTranslation.Persian;

                
            var ingredientIds =
                await _mediator.Send(new GetFoodIngredientIdsQuery(dietPackFood.FoodId), cancellationToken);

            foreach (var ingredientId in ingredientIds)
            {
                var allergy = await _mediator.Send(new GetIngredientAllergyIdsQuery(ingredientId),
                    cancellationToken);
                if(!string.IsNullOrEmpty(allergy))
                    allergyCategoriesListIds.Add(allergy);
            }

            allergyCategoriesListIds = allergyCategoriesListIds.Distinct().ToList();

            foreach (var nationalityId in dto.NationalityIds)
            {
                var isNationalityExits =
                    await _mediator.Send(new IsNationalityExitsQuery(nationalityId), cancellationToken);
                if (!isNationalityExits)
                    throw new BadRequestException("NationalityId not exits");
            }

            foreach (var dietCategoryId in dto.DietCategoryIds)
            {
                var dietCategoryIdExits =
                    await _mediator.Send(new IsDietCategoryExitsQuery(dietCategoryId), cancellationToken);
                if (!dietCategoryIdExits)
                    throw new BadRequestException("DietCategoryId not exits");
            }
            var measureUnit = await _mediator.Send(new GetMeasureUnitByIdQuery(dietPackFood.MeasureUnitId),cancellationToken);
            dietPackFood.MeasureUnitName = measureUnit.Name.Persian;
            dietPackFood.MeasureUnitValue = measureUnit.Value;
        }

        await _mediator.Send(new UpdateDietPackCommand(dto.Id, new CreateDietPackTranslationDto
            {
                Persian = persian,
                Arabic = arabic,
                English = english
            }, dto.FoodMeal, dto.SpecialDiseases, dto.CalorieValue, dto.NutrientValues, dto.IsActive,
            dto.DietCategoryIds, dto.DailyCalorie,
            dto.ParentCategoryId, dto.NationalityIds, dto.DietPackFoods, allergyCategoriesListIds),cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.SoftDeleteDietPack)]
    [HttpDelete("soft-delete-diet-pack")]
    public async Task<ActionResult> SoftDeleteDietPack([FromQuery] string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteDietPackCommand(id),cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

}