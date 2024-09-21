using Common.Constants.Food;
using Food.V2.Application.Dtos.PersonalFood;
using Food.V2.Application.Ingredients.V1.Queries.GetCalculatedIngredient;
using Food.V2.Domain.Aggregates.PersonalFoodAggregate;

namespace Food.V2.Application.PersonalFoods.V1.Commands.InsertPersonalFood;

public class InsertPersonalFoodCommandHandler : IRequestHandler<InsertPersonalFoodCommand, PersonalFoodDto>
{
    private readonly IUnitOfWork _work;
    private readonly IFileService _fileService;
    private readonly IMediator _mediator;

    public InsertPersonalFoodCommandHandler(IUnitOfWork work, IFileService fileService, IMediator mediator)
    {
        _work = work;
        _fileService = fileService;
        _mediator = mediator;
    }

    public async Task<PersonalFoodDto> Handle(InsertPersonalFoodCommand request, CancellationToken cancellationToken)
    {
        var personalFood = new PersonalFood
        {
            BakingType = request.BakingType,
            Name = request.Name,
            BakingTime = request.BakingTime,
            AppId = request.AppId,
            ParentFoodId = request.ParentFoodId!.StringToObjectId(),
        };
        var calculateIngredients = await _mediator.Send(new GetCalculatedIngredientQuery(request.Ingredients));
        if (!string.IsNullOrWhiteSpace(request.ImageUri))
        {
            personalFood.ImageName = _fileService.AddImage(request.ImageUri, "PersonalFoodImage",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        var foodFactors = new FoodParametersDto
        {
            BakingTimeInMinute = Convert.ToDecimal(request.BakingTime.TotalMinutes),
            BakingRatio = Convert.ToDecimal(request.BakingType.GetDescriptionValue()),
            FoodNutrients = calculateIngredients.CalculatedIngredient,
            FoodWeight = calculateIngredients.SumWeight
        }.FoodWeightsCalculation();
        var remainedWater = calculateIngredients.CalculatedIngredient[1] - foodFactors.EvaporatedWater;
        if (remainedWater > 0)
        {
            personalFood.WeightBeforeBaking = new NotNegativeForDecimalTypes(calculateIngredients.SumWeight);
            personalFood.EvaporatedWater = new NotNegativeForDecimalTypes(foodFactors.EvaporatedWater);
            personalFood.WeightAfterBaking = new NotNegativeForDecimalTypes(foodFactors.AfterBaking);
            List<NotNegativeForDecimalTypes> foodNutrients = new List<NotNegativeForDecimalTypes>();
            foreach (var nut in calculateIngredients.CalculatedIngredient)
            {
                foodNutrients.Add(new NotNegativeForDecimalTypes(nut * 100 / foodFactors.AfterBaking));
            }

            foodNutrients[1] = new NotNegativeForDecimalTypes(remainedWater * 100 / foodFactors.AfterBaking);
            personalFood.NutrientValue = foodNutrients;
            personalFood.IsCopyable = false;
            //Add PersonalFood Ingredient
            var foodIngredients = new List<PersonalFoodIngredient>();
            foreach (var i in request.Ingredients)
            {
                var ingredient = await _work.GenericRepository<Ingredient>()
                    .GetByIdAsync(i.IngredientId, cancellationToken);
                ingredient!.MeasureUnitIds.Add(i.MeasureUnitId.StringToObjectId()); //for food Ingredient
                var measureUnitIds = ingredient.MeasureUnitIds.ConvertAll(x => x.ToString());
                var filter =
                    Builders<MeasureUnit>.Filter.Where(x =>
                        measureUnitIds.Contains(x.Id)); // for IngredientMeasureUnits
                var ingredientMeasureUnit = await _work.GenericRepository<MeasureUnit>()
                    .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
                var foodIng = new PersonalFoodIngredient()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    IngredientId = i.IngredientId.StringToObjectId(),
                    IngredientValue = new NotNegativeForDecimalTypes(i.Value),
                    MeasureUnitId =
                        ingredientMeasureUnit.FirstOrDefault(x => x.Id == i.MeasureUnitId)!.Id.StringToObjectId(),
                    MeasureUnitTranslation = ingredientMeasureUnit.FirstOrDefault(x => x.Id == i.MeasureUnitId)!
                        .Translation
                        .MapTo<PersonalFoodTranslation, MeasureUnitTranslation>(),
                    MeasureUnitValue = ingredientMeasureUnit.FirstOrDefault(x => x.Id == i.MeasureUnitId)!.Value,
                    IngredientTranslation =
                        ingredient.Translation.MapTo<PersonalFoodTranslation, IngredientTranslation>(),
                    IsDelete = false,
                    IngredientMeasureUnits = ingredientMeasureUnit.Select(x => new IngredientMeasureUnit
                    {
                        Id = x.Id,
                        Translation = x.Translation.ToDto<FoodTranslation>(),
                        IsActive = x.IsActive,
                        Value = x.Value,
                        IsDelete = false
                    }).ToList()
                };

                foodIngredients.Add(foodIng);
            }

            personalFood.PersonalFoodIngredients = foodIngredients;
            await _work.GenericRepository<PersonalFood>()
                .InsertOneAsync(personalFood, null, cancellationToken);

            var measureUnits = new List<string> { Keys.Grams, Keys.Ounces, Keys.Pounds };
            if (personalFood.ParentFoodId!=ObjectId.Empty)
            {
                var filter = Builders<Domain.Aggregates.FoodAggregate.Food>.Filter.Eq(x => x.Id, request.ParentFoodId);
                var personalFoodIngredients =
                    await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
                        .GetListOfDocumentsByFilterAsync(filter, cancellationToken);

                if (personalFoodIngredients.Any())
                {
                    measureUnits.AddRange(personalFoodIngredients
                        .Select(m => m.MeasureUnitIds.ConvertAll(x => x.ToString()).ToList()).SingleOrDefault()!);
                }
            }

            var result = personalFood.ToDto<PersonalFoodDto>();
            result.BakingTypeName = result.BakingType.ToDescription();
            result.MeasureUnits = measureUnits.Distinct().ToList();
            return result;
        }

        return null;
    }
}