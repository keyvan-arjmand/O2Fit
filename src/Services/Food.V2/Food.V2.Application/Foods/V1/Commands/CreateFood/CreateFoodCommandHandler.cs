namespace Food.V2.Application.Foods.V1.Commands.CreateFood;

public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;

    public CreateFoodCommandHandler(IUnitOfWork uow, IFileService fileService)
    {
        _uow = uow;
        _fileService = fileService;
    }

    public async Task Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var imageName = string.Empty;
        var thumbName = string.Empty;
        if (!string.IsNullOrEmpty(request.ImageUri))
            imageName = _fileService.AddImage(request.ImageUri, PathAddressesConstants.FoodImages, request.FoodCode);

        if (!string.IsNullOrEmpty(request.ImageThumb))
            thumbName = _fileService.AddImage(request.ImageThumb, PathAddressesConstants.FoodThumb, request.FoodCode);

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

        var food = new Domain.Aggregates.FoodAggregate.Food
        {
            BakingTime = request.BakingTime,
            BakingType = request.BakingType,
            BarcodeGs1 = request.BarcodeGs1, //new BarCodeGs1(request.BarcodeGs1),
            BarcodeNational = request.BarcodeNational, // new BarCodeNational(request.BarcodeNational),
            BrandId = string.IsNullOrEmpty(request.BrandId) ? ObjectId.Empty : ObjectId.Parse(request.BrandId),
            DryIngredient = dryIngredient, //foodFactors.DryIngredient,
            EvaporatedWater = evaporatedWater, //foodFactors.EvaporatedWater,
            FoodCode = request.FoodCode,
            FoodType = request.FoodType,
            ImageThumb = thumbName,
            ImageUri = imageName,
            IsActive = request.IsActive,
            Name = request.Name.ToEntity<FoodTranslation>(),
            NutrientValue = foodNutrients.Count > 0
                ? foodNutrients.Select(s => new NotNegativeForDecimalTypes(s)).ToList()
                : request.NutrientValue.Select(x => new NotNegativeForDecimalTypes(x)).ToList(),
            PersonCount = request.PersonCount,
            Tag = request.Tag,
            TagArEn = request.TagArEn,
            WeightAfterBaking =
                new NotNegativeForDecimalTypes(afterBaking), //new NotNegativeForDecimalTypes(foodFactors.AfterBaking),
            WeightBeforeBaking =
                new NotNegativeForDecimalTypes(
                    beforeBaking), //new NotNegativeForDecimalTypes(foodFactors.BeforeBaking),
            Gi = new NotNegativeForDoubleTypes(request.Gi),
            UseInDiet = request.UseInDiet,
            DefaultMeasureUnitId = string.IsNullOrEmpty(request.DefaultMeasureUnitId)
                ? ObjectId.Parse(DefaultMeasureUnitConstants.GramsId)
                : ObjectId.Parse(request.DefaultMeasureUnitId),
            FoodMeals = request.FoodMeals,
            RecipeCategoryId = ObjectId.Parse(request.RecipeCategoryId),
            MeasureUnitIds = measureUnitsObjectIds,
            NationalityIds = nationalityObjectIds,
            FoodCategoryIds = foodCategoryObjectIds,
            DietCategoryIds = dietCategoryObjectIds,
            FoodHabits = request.FoodHabits,
            SpecialDiseases = request.SpecialDiseases,
            FoodIngredients = foodIngredients
        };
        await _uow.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .InsertOneAsync(food, null, cancellationToken);
    }
}