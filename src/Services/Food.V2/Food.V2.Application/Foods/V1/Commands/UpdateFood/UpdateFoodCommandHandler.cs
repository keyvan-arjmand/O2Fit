using Food.V2.Application.Common.ApiResult;

namespace Food.V2.Application.Foods.V1.Commands.UpdateFood;

public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;

    public UpdateFoodCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        var oldFood = await _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (oldFood == null)
            throw new NotFoundException(nameof(Domain.Aggregates.FoodAggregate.Food), request.Id);

        if (!string.IsNullOrEmpty(request.ImageUri))
            _fileService.RemoveImage(oldFood.FoodCode, PathAddressesConstants.FoodImages);

        if (!string.IsNullOrEmpty(request.ImageThumb))
            _fileService.RemoveImage(oldFood.FoodCode, PathAddressesConstants.FoodThumb);


        var imageName = string.Empty;
        var thumbName = string.Empty;
        if (!string.IsNullOrEmpty(request.ImageUri))
            imageName = _fileService.AddImage(request.ImageUri, PathAddressesConstants.FoodImages, request.FoodCode);

        if (!string.IsNullOrEmpty(request.ImageThumb))
            thumbName = _fileService.AddImage(request.ImageThumb, PathAddressesConstants.FoodThumb, request.FoodCode);

        #region comment

        // var foodNutrients = new List<decimal>();
        //
        // var foodFactors = Formula.FoodWeightsCalculation(new FoodParametersDto()
        // {
        //     FoodWeight = request.SumWeight,
        //     BakingRatio = Convert.ToDecimal((request.BakingType).ToDescription()),
        //     BakingTimeInMinute = Convert.ToDecimal(request.BakingTime.TotalMinutes),
        //     FoodNutrients = request.CalculatedIngredient.Count> 0 ? request.CalculatedIngredient: request.Nutrients
        // });
        //
        //
        // if (foodFactors.AfterBaking - foodFactors.DryIngredient <= 0
        //     && request.FoodType != FoodType.Supermarket &&
        //     request.BakingType != BakingType.NoTypeOfBaking)
        //     throw new BadRequestException("Your food is burnt");
        //
        //
        // foreach (var nut in request.CalculatedIngredient)
        // {
        //     foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
        // }
        //
        // foodNutrients[1] = (foodFactors.AfterBaking - foodFactors.DryIngredient) * 100 /
        //                    foodFactors.AfterBaking;
        //
        // if (foodNutrients[1] <= 0 &&
        //     request.FoodType != FoodType.Supermarket &&
        //     request.BakingType != BakingType.NoTypeOfBaking)
        // {
        //     throw new BadRequestException( "Your food is burnt");
        // }
        //
        // if (request.FoodType == FoodType.Supermarket)
        // {
        //     foodNutrients = request.Nutrients;
        // }

        #endregion

        var foodNutrients = new List<decimal>();
        decimal beforeBaking = 0;
        decimal afterBaking = 0;
        decimal evaporatedWater = 0;
        decimal dryIngredient = 0;
        decimal water = 0;

        if (request.FoodType != FoodType.Supermarket)
        {
            var foodFactors = Formula.FoodWeightsCalculation(new FoodParametersDto
            {
                FoodWeight = request.SumWeight,
                BakingRatio = Convert.ToDecimal(request.BakingType.ToDescription()),
                BakingTimeInMinute = Convert.ToDecimal(request.BakingTime.TotalMinutes),
                FoodNutrients = request.CalculatedIngredient
            });

            beforeBaking = foodFactors.BeforeBaking;
            afterBaking = foodFactors.AfterBaking;
            evaporatedWater = foodFactors.EvaporatedWater;
            dryIngredient = foodFactors.DryIngredient;
            water = foodFactors.Water;

            if (foodFactors.AfterBaking - foodFactors.DryIngredient <= 0
                && request.FoodType != FoodType.Supermarket &&
                request.BakingType != BakingType.NoTypeOfBaking)
                throw new BadRequestException("Your food is burnt");

            var nutrients = request.CalculatedIngredient;

            if (request.FoodType != FoodType.Supermarket && foodFactors.AfterBaking > 0)
            {
                foreach (var nut in request.CalculatedIngredient)
                    foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);

                foodNutrients[1] = (foodFactors.AfterBaking - foodFactors.DryIngredient) * 100 /
                                   foodFactors.AfterBaking;
            }
        }

        var measureUnitsObjectIds = new List<ObjectId>();
        measureUnitsObjectIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.GramsId));
        measureUnitsObjectIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.OuncesId));
        measureUnitsObjectIds.Add(ObjectId.Parse(DefaultMeasureUnitConstants.PoundsId));

        if (request.MeasureUnitIds.Count > 0)
        {
            foreach (var mUnitId in request.MeasureUnitIds)
                measureUnitsObjectIds.Add(ObjectId.Parse(mUnitId));
        }

        measureUnitsObjectIds = measureUnitsObjectIds.Distinct().ToList();

        var nationalityObjectIds = new List<ObjectId>();
        if (request.NationalityIds.Count > 0)
        {
            foreach (var nationalityId in request.NationalityIds)
                nationalityObjectIds.Add(ObjectId.Parse(nationalityId));
        }

        var foodCategoryObjectIds = new List<ObjectId>();
        if (request.FoodCategoryIds.Count > 0)
        {
            foreach (var foodCategoryId in request.FoodCategoryIds)
                foodCategoryObjectIds.Add(ObjectId.Parse(foodCategoryId));
        }

        var dietCategoryObjectIds = new List<ObjectId>();
        if (request.DietCategoryIds.Count > 0)
        {
            foreach (var dietCategoryId in request.DietCategoryIds)
                dietCategoryObjectIds.Add(ObjectId.Parse(dietCategoryId));
        }


        oldFood.WeightBeforeBaking = new NotNegativeForDecimalTypes(beforeBaking);
        oldFood.EvaporatedWater = evaporatedWater;
        oldFood.WeightAfterBaking = new NotNegativeForDecimalTypes(afterBaking);
        oldFood.BakingTime = request.BakingTime;
        oldFood.DryIngredient = dryIngredient;

        oldFood.NutrientValue = foodNutrients.Count > 0
            ? foodNutrients.Select(s => new NotNegativeForDecimalTypes(s)).ToList()
            : request.Nutrients.Select(x => new NotNegativeForDecimalTypes(x))
                .ToList(); //foodNutrients.Select(s => new NotNegativeForDecimalTypes(s)).ToList();
        oldFood.BakingType = request.BakingType;
        oldFood.FoodType = request.FoodType;
        oldFood.BrandId = string.IsNullOrEmpty(request.BrandId)
            ? ObjectId.Empty
            : ObjectId.Parse(request.BrandId);
        oldFood.BarcodeGs1 = request.BarcodeGs1;
        oldFood.BarcodeNational = request.BarcodeNational;
        oldFood.IsActive = request.IsActive;
        oldFood.FoodCode = request.FoodCode;
        oldFood.PersonCount = request.PersonCount;
        oldFood.Tag = request.Tag.ToLower();
        oldFood.TagArEn = string.IsNullOrEmpty(request.TagArEn) ? string.Empty : request.TagArEn.ToLower();
        oldFood.Gi = new NotNegativeForDoubleTypes(request.Gi);
        oldFood.UseInDiet = request.UseInDiet;
        oldFood.DefaultMeasureUnitId = ObjectId.Parse(request.DefaultMeasureUnitId);
        oldFood.FoodMeals = request.FoodMeals;
        oldFood.RecipeCategoryId = ObjectId.Parse(request.RecipeCategoryId);
        oldFood.FoodCategoryIds = foodCategoryObjectIds;
        oldFood.NationalityIds = nationalityObjectIds;
        oldFood.FoodHabits = request.FoodHabits;
        oldFood.DietCategoryIds = dietCategoryObjectIds;
        oldFood.SpecialDiseases = request.SpecialDiseases;
        oldFood.MeasureUnitIds = measureUnitsObjectIds;
        oldFood.ImageUri = imageName;
        oldFood.ImageThumb = thumbName;
        oldFood.Name.Persian = request.Name.Persian;
        oldFood.Name.Arabic = request.Name.Arabic;
        oldFood.Name.English = request.Name.English;
        
        //add FoodIngredient
        List<FoodIngredient> foodIngredients = new List<FoodIngredient>();
        foreach (var i in request.Ingredients)
        {
            var ingredient = await _uow.GenericRepository<Ingredient>()
                .GetByIdAsync(i.IngredientId, cancellationToken);
            ingredient!.MeasureUnitIds.Add(i.MeasureUnitId.StringToObjectId()); //for food Ingredient
            var filter =
                Builders<MeasureUnit>.Filter.Where(x => ingredient!.MeasureUnitIds.Contains(ObjectId.Parse(x.Id))); // for IngredientMeasureUnits
            var ingredientMeasureUnit = await _uow.GenericRepository<MeasureUnit>()
                .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

            foodIngredients.Add(new FoodIngredient
            {
                IngredientId = ingredient!.Id.StringToObjectId(),
                MeasureUnitId =
                    ingredientMeasureUnit.FirstOrDefault(x => x.Id == i.MeasureUnitId)!.Id.StringToObjectId(),
                IngredientValue = new NotNegativeForDecimalTypes(i.Value),
                MeasureUnitValue = ingredientMeasureUnit.FirstOrDefault(x => x.Id == i.MeasureUnitId)!.Value,
                IngredientMeasureUnits = ingredientMeasureUnit.Select(x => new IngredientMeasureUnit
                {
                    Translation = x.Translation.ToDto<FoodTranslation>(),
                    IsActive = x.IsActive,
                    Value = x.Value,
                    IsDelete = false
                }).ToList(),
                IngredientTranslation = ingredient.Translation.MapTo<FoodTranslation, IngredientTranslation>(),
                MeasureUnitTranslation = ingredientMeasureUnit.FirstOrDefault(x => x.Id == i.MeasureUnitId)!.Translation
                    .MapTo<FoodTranslation, MeasureUnitTranslation>()
            });
        }
        oldFood.FoodIngredients = foodIngredients;
        await _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().UpdateOneAsync(x => x.Id == request.Id,
            oldFood, new Expression<Func<Domain.Aggregates.FoodAggregate.Food, object>>[]
            {
                x => x.WeightBeforeBaking,
                x => x.EvaporatedWater,
                x => x.WeightAfterBaking,
                x => x.BakingTime,
                x => x.DryIngredient,
                x => x.NutrientValue,
                x => x.BakingType,
                x => x.FoodType,
                x => x.BrandId,
                x => x.BarcodeGs1,
                x => x.BarcodeNational,
                x => x.IsActive,
                x => x.FoodCode,
                x => x.PersonCount,
                x => x.Tag,
                x => x.TagArEn,
                x => x.Gi,
                x => x.UseInDiet,
                x => x.DefaultMeasureUnitId,
                x => x.FoodMeals,
                x => x.RecipeCategoryId,
                x => x.FoodCategoryIds,
                x => x.NationalityIds,
                x => x.FoodHabits,
                x => x.DietCategoryIds,
                x => x.SpecialDiseases,
                x => x.MeasureUnitIds,
                x => x.ImageUri,
                x => x.ImageThumb,
                x => x.Name.Persian,
                x => x.Name.Arabic,
                x => x.Name.English,
                x => x.FoodIngredients
            }, null, cancellationToken);
    }
}