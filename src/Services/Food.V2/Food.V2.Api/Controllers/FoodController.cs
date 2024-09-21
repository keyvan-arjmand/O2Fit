using Food.V2.Application.Dtos.Ingredients;
using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Application.Foods.V1.Commands.CreateFood;
using Food.V2.Application.Foods.V1.Queries.CheckDuplicateBarCode;
using Food.V2.Application.Foods.V1.Queries.CheckDuplicateFoodCode;
using Food.V2.Application.Foods.V1.Queries.GetAllActiveFoodQuery;
using Food.V2.Application.Foods.V1.Queries.GetFoodByBarcode;
using Food.V2.Application.Foods.V1.Queries.GetFoodByFilter;
using Food.V2.Application.Foods.V1.Queries.GetFoodById;
using Food.V2.Application.Foods.V1.Queries.GetFoodByName;
using Food.V2.Application.Foods.V1.Queries.GetFoodWithDetailById;
using Food.V2.Application.Ingredients.V1.Queries.GetCalculatedIngredient;
using Food.V2.Application.Recipes.V1.Commands.ChangeRecipeStatus;
using Food.V2.Application.Recipes.V1.Commands.SoftDeleteRecipe;
using Food.V2.Application.Recipes.V1.Queries.GetAllPagingRecipe;
using Food.V2.Application.Recipes.V1.Queries.GetFullRecipeById;
using Food.V2.Application.RecipeSteps.V1.Commands.CreateRecipeSteps;
using Food.V2.Application.RecipeSteps.V1.Commands.PatchRecipeStep;
using Food.V2.Application.RecipeSteps.V1.Commands.SoftDeleteRecipeStep;
using Food.V2.Application.RecipeSteps.V1.Queries.GetAllRecipeSteps;
using Food.V2.Application.RecipeSteps.V1.Queries.GetByIdRecipeStep;
using Food.V2.Application.RecipeSteps.V1.Queries.GetRecipeStepsByFoodId;
using Food.V2.Application.RecipeTips.V1.Commands.CreateRecipeTips;
using Food.V2.Application.RecipeTips.V1.Commands.PatchRecipeTip;
using Food.V2.Application.RecipeTips.V1.Commands.SoftDeleteRecipeTip;
using Food.V2.Application.RecipeTips.V1.Queries.GetAllRecipeTips;
using Food.V2.Application.RecipeTips.V1.Queries.GetByIdRecipeTip;
using Food.V2.Application.RecipeTips.V1.Queries.GetRecipeTipsByFoodId;
using Food.V2.Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Food.V2.Api.Controllers;

[ApiVersion("1")]
public class FoodController : BaseApiController
{
    private readonly IMediator _mediator;

    public FoodController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HasPermission(PermissionsConstants.GetAllActiveRecipes)]
    [HttpGet("get-all-active-recipe-foods")]
    public async Task<List<FullRecipeDto>> GetAllActiveRecipes(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllActiveRecipeFoodsQuery(), cancellationToken);
    }

    [HasPermission(PermissionsConstants.GetRecipeFoodWithDetailById)]
    [HttpGet("get-recipe-food-with-detail-by-id")]
    public async Task<FullRecipeDto> GetRecipeFoodWithDetailById(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFoodWithDetailByIdQuery(id), cancellationToken);
    }

    [HasPermission(PermissionsConstants.GetAllRecipePaging)]
    [HttpGet("get-all-paging-recipe")]
    public async Task<IActionResult> GetAllRecipePaging(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllPagingRecipeQuery(page, pageSize), cancellationToken);
        return Ok(new ApiResult<PaginationResult<RecipeDto>>(result, string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.GetFullRecipeById)]
    [HttpGet("get-recipe-by-id")]
    public async Task<FullRecipeDto> GetFullRecipeById(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetFullRecipeByIdQuery(id), cancellationToken);
    }

    [HasPermission(PermissionsConstants.GetAllRecipeTips)]
    [HttpGet("get-all-recipe-tip")]
    public async Task<List<RecipeTipDto>> GetAllRecipeTips(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllRecipeTipsQuery(), cancellationToken);
    }

    [HasPermission(PermissionsConstants.GetByIdRecipe)]
    [HttpGet("get-by-id-recipe-tip")]
    public async Task<RecipeTipSingleDto> GetByIdRecipe(string id, string foodId, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetByIdRecipeTipQuery(id, foodId), cancellationToken);
    }

    [HttpGet("get-recipe-tip-by-food-id")]
    [HasPermission(PermissionsConstants.GetRecipeStepsByFoodId)]
    public async Task<List<TranslationDto>> GetRecipeStepsByFoodId(string id, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetRecipeTipsByFoodIdQuery(id), cancellationToken);
    }

    [HasPermission(PermissionsConstants.SearchFoodByFilter)]
    [HttpGet("search-food-by-filter-paging")]
    public async Task<List<FilterFoodDto>> SearchFoodByFilter([FromQuery] FoodSearchFilter request,
        CancellationToken cancellationToken)
    {
        string lang = Request.Headers.FirstOrDefault(x => x.Key.Equals("Language")).Value;
        request.Language = string.IsNullOrEmpty(lang) ? "Persian" : lang;
        return await _mediator.Send(new GetFoodByFilterQuery
        {
            Filter = request,
        }, cancellationToken);
    }

    [HasPermission(PermissionsConstants.GetFoodById)]
    [HttpGet("get-food-by-id")]
    public async Task<GetUserFoodDto> GetFoodById(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetFoodByIdQuery(id), cancellationToken);
        return result.MapTo<GetUserFoodDto, FoodWithDetailDto>();
    }
    //todo 

    // [HasPermission(PermissionsConstants.CreateNationality)]
    // [HttpGet("get-foods-by-body-type")]
    // public async Task<ActionResult> GetFoodsByBodyType(BodyType type, CancellationToken cancellationToken)
    // {
    //     var result = await _mediator.Send(new GetFoodsByBodyTypeQuery(type), cancellationToken);
    //     return Ok(new ApiResult<PaginationResult<List<int>>>(result, string.Empty, ApiResultStatusCode.Success,
    //         true));
    // }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("search-food-by-name")]
    public async Task<ActionResult> Search(int page, int pageSize, string name, FoodType foodType,
        CancellationToken cancellationToken)
    {
        var lang = LanguageName ?? "Persian";
        var result = await _mediator.Send(new GetFoodByNameQuery(page, pageSize, name, lang),
            cancellationToken);
        return Ok(new ApiResult<PaginationResult<SearchFoodNameDto>>(result, string.Empty, ApiResultStatusCode.Success,
            true));
    }

    [HasPermission(PermissionsConstants.CreateNationality)]
    [HttpGet("search-food-by-barcode")]
    public async Task<SearchFoodNameDto> SearchFoodByBarcode(string barcode, CancellationToken cancellationToken)
    {
        string lang = Request.Headers.FirstOrDefault(x => x.Key.Equals("Language")).Value!;
        return await _mediator.Send(new GetFoodByBarcodeQuery(barcode, lang ?? "Persian"), cancellationToken);
    }

    [HasPermission(PermissionsConstants.CreateRecipeSteps)]
    [HttpPost("recipe-tip")]
    public async Task<IActionResult> CreateRecipeSteps(CreateRecipeTipsCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateRecipeSteps)]
    [HttpPost("recipe-steps")]
    public async Task<IActionResult> CreateRecipeSteps(CreateRecipeStepsCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreateFood)]
    [HttpPost("create-food")]
    public async Task<ActionResult> CreateFood(CreateFoodDto dto, CancellationToken cancellationToken)
    {
        var isFoodCodeExists = await _mediator.Send(new CheckDuplicateFoodCodeQuery(dto.FoodCode), cancellationToken);
        if (isFoodCodeExists)
            throw new BadRequestException("FoodCode is duplicated");

        if (dto.MeasureUnitIds.Count > 0)
        {
            foreach (var measureUnitId in dto.MeasureUnitIds)
            {
                var measureUnitResult =
                    await _mediator.Send(new IsMeasureUnitExitsQuery(measureUnitId), cancellationToken);
                if (!measureUnitResult)
                    throw new NotFoundException(nameof(MeasureUnit), measureUnitId);
            }
        }

        if (dto.NationalityIds.Count > 0)
        {
            foreach (var nationalityId in dto.NationalityIds)
            {
                var nationalityResult =
                    await _mediator.Send(new IsNationalityExitsQuery(nationalityId), cancellationToken);
                if (!nationalityResult)
                    throw new NotFoundException(nameof(Nationality), nationalityId);
            }
        }

        if (dto.FoodCategoryIds.Count > 0)
        {
            foreach (var foodCategoryId in dto.FoodCategoryIds)
            {
                var foodCategoryResult =
                    await _mediator.Send(new IsFoodCategoryExitsQuery(foodCategoryId), cancellationToken);
                if (!foodCategoryResult)
                    throw new NotFoundException(nameof(Category), foodCategoryId);
            }
        }

        if (dto.DietCategoryIds.Count > 0)
        {
            foreach (var dietCategoryId in dto.DietCategoryIds)
            {
                var dietCategoryResult =
                    await _mediator.Send(new IsDietCategoryExitsQuery(dietCategoryId), cancellationToken);
                if (!dietCategoryResult)
                    throw new NotFoundException(nameof(DietCategory), dietCategoryId);
            }
        }

        if (!string.IsNullOrEmpty(dto.DefaultMeasureUnitId))
        {
            var measureUnitResult =
                await _mediator.Send(new IsMeasureUnitExitsQuery(dto.DefaultMeasureUnitId), cancellationToken);
            if (!measureUnitResult)
                throw new NotFoundException(nameof(MeasureUnit), dto.DefaultMeasureUnitId);
        }

        if (!string.IsNullOrEmpty(dto.RecipeCategoryId))
        {
            var recipeCategoryResult =
                await _mediator.Send(new IsRecipeCategoryExistsQuery(dto.RecipeCategoryId), cancellationToken);
            if (!recipeCategoryResult)
                throw new NotFoundException(nameof(RecipeCategory), dto.RecipeCategoryId);
        }

        var calc = new CalculatedIngredientDto();
        if (dto.FoodType == FoodType.Homemade ||
            dto.FoodType == FoodType.Restaurant)
        {
            if (!dto.Ingredients.Any() && dto.FoodType != FoodType.Supermarket)
                throw new BadRequestException("Ingredients Is Required");

            calc = await _mediator.Send(new GetCalculatedIngredientQuery(dto.Ingredients), cancellationToken);
        }
        else if (dto.FoodType == FoodType.Supermarket)
        {
            if (string.IsNullOrEmpty(dto.BarcodeGs1))
                throw new BadRequestException("BarCodeGs1 is required");

            if (dto.BrandId.Equals(ObjectId.Empty))
                throw new BadRequestException("BrandId is required");

            var isBrandExits = await _mediator.Send(new IsBrandExitsQuery(dto.BrandId), cancellationToken);
            if (!isBrandExits)
                throw new BadRequestException("BrandId not exits");
        }
        else
        {
            var isBarCodeExists =
                await _mediator.Send(new CheckDuplicateBarCodeQuery(dto.BarcodeGs1, dto.BarcodeNational),
                    cancellationToken);

            if (isBarCodeExists)
                throw new BadRequestException("Barcode is duplicated");

            calc.SumWeight = 100;
            calc.CalculatedIngredient = dto.Nutrients.Select(Convert.ToDecimal).ToList();
            dto.BakingTime = new TimeSpan(0);
        }

        await _mediator.Send(new CreateFoodCommand(calc.CalculatedIngredient, calc.SumWeight, dto.FoodCode,
            dto.BakingType, dto.BakingTime, dto.BarcodeGs1,
            dto.BarcodeNational, dto.ImageUri, dto.ImageThumb, dto.FoodType, dto.IsActive, dto.Tag, dto.TagArEn,
            dto.Nutrients, dto.BrandId,
            dto.PersonCount, dto.FoodMeals, dto.Gi, dto.UseInDiet, dto.FoodHabits, dto.DefaultMeasureUnitId,
            dto.RecipeCategoryId, dto.NationalityIds,
            dto.FoodCategoryIds, dto.DietCategoryIds, dto.MeasureUnitIds, dto.Ingredients, dto.Name,
            dto.SpecialDiseases), cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.CreatePermission)]
    [HttpPut("update-food")]
    public async Task<ActionResult> UpdateFood(UpdateFoodDto dto, CancellationToken cancellationToken)
    {
        var isFoodExists = await _mediator.Send(new IsFoodExistsQuery(dto.Id), cancellationToken);
        if (!isFoodExists)
            throw new NotFoundException(nameof(Domain.Aggregates.FoodAggregate.Food), dto.Id);


        var calc = new CalculatedIngredientDto();
        if (dto.FoodType == FoodType.Homemade ||
            dto.FoodType == FoodType.Restaurant)
        {
            if (!dto.Ingredients.Any() && dto.FoodType != FoodType.Supermarket)
                throw new BadRequestException("Ingredients Is Required");

            calc = await _mediator.Send(new GetCalculatedIngredientQuery(dto.Ingredients), cancellationToken);
        }
        else if (dto.FoodType == FoodType.Supermarket)
        {
            if (string.IsNullOrEmpty(dto.BarcodeGs1))
                throw new BadRequestException("BarCodeGs1 is required");

            if (dto.BrandId.Equals(ObjectId.Empty))
                throw new BadRequestException("BrandId is required");

            var isBrandExits = await _mediator.Send(new IsBrandExitsQuery(dto.BrandId), cancellationToken);
            if (!isBrandExits)
                throw new BadRequestException("BrandId not exits");
        }
        else
        {
            var isBarCodeExists =
                await _mediator.Send(new CheckDuplicateBarCodeQuery(dto.BarcodeGs1, dto.BarcodeNational),
                    cancellationToken);

            if (isBarCodeExists)
                throw new BadRequestException("Barcode is duplicated");

            calc.SumWeight = 100;
            calc.CalculatedIngredient = dto.Nutrients.Select(Convert.ToDecimal).ToList();
            dto.BakingTime = new TimeSpan(0);
        }

        await _mediator.Send(new UpdateFoodCommand(dto.Id, dto.Name, dto.FoodCode, dto.BakingType, dto.BakingTime,
            dto.BarcodeGs1, dto.BarcodeNational, dto.FoodHabits,
            dto.SpecialDiseases, dto.ImageUri, dto.ImageThumb, dto.FoodType, dto.BrandId, dto.Nutrients,
            dto.MeasureUnitIds, dto.Ingredients, dto.Tag, dto.TagArEn,
            dto.PersonCount, dto.IsActive, dto.UseInDiet, dto.NationalityIds, dto.FoodMeals, dto.FoodCategoryIds,
            dto.DietCategoryIds, dto.DefaultMeasureUnitId,
            dto.RecipeCategoryId, calc.CalculatedIngredient, calc.SumWeight, dto.Gi), cancellationToken);

        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.ChangeRecipeStatus)]
    [HttpPatch("change-recipe-status")]
    public async Task<IActionResult> ChangeRecipeStatus(ChangeRecipeStatusCommand request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateStep)]
    [HttpPatch("update-steps")]
    public async Task<IActionResult> UpdateStep(PatchRecipeStepCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.UpdateTip)]
    [HttpPatch("update-tip")]
    public async Task<IActionResult> UpdateTip(PatchRecipeTipCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.SoftDeleteRecipe)]
    [HttpDelete("soft-delete-recipe")]
    public async Task<IActionResult> SoftDeleteRecipe(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteRecipeCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.SoftDeleteFood)]
    [HttpDelete("soft-delete-food")]
    public async Task<ActionResult> SoftDeleteFood(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteFoodCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    [HasPermission(PermissionsConstants.SoftDeleteRecipeStep)]
    [HttpDelete("soft-delete-tip-step")]
    public async Task<IActionResult> SoftDeleteRecipeStep(string id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new SoftDeleteRecipeTipCommand(id), cancellationToken);
        return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
    }

    //
    // [HttpGet("get-version")]
    // public async Task<ActionResult> GetAll(double lastVersion, CancellationToken cancellationToken)
    // {
    //     var result = await _mediator.Send(new SoftDeleteRecipeTipCommand(id), cancellationToken);
    //     return Ok(new ApiResult<PaginationResult<SearchFoodNameDto>>(result, string.Empty, ApiResultStatusCode.Success, 
    // }
    // [HttpPut("update-food")]
    //public async Task<ActionResult> UpdateFood(UpdateFoodDto dto, CancellationToken cancellationToken)
    //{
    //    
    //}
}