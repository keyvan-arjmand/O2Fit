using Common.Constants.Food;
using Food.V2.Application.Dtos.PersonalFood;
using Food.V2.Application.Ingredients.V1.Queries.GetCalculatedIngredient;
using Food.V2.Domain.Aggregates.PersonalFoodAggregate;

namespace Food.V2.Application.PersonalFoods.V1.Commands.UpdatePersonalFood;

public class UpdatePersonalFoodCommandHandler : IRequestHandler<UpdatePersonalFoodCommand, PersonalFoodDto>
{
    private readonly IUnitOfWork _work;
    private readonly IFileService _fileService;
    private readonly IMediator _mediator;

    public UpdatePersonalFoodCommandHandler(IUnitOfWork work, IFileService fileService, IMediator mediator)
    {
        _work = work;
        _fileService = fileService;
        _mediator = mediator;
    }

    public async Task<PersonalFoodDto> Handle(UpdatePersonalFoodCommand request, CancellationToken cancellationToken)
    {
        var personalFood = await _work.GenericRepository<PersonalFood>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (personalFood == null) throw new NotFoundException($"personalFood not Found{request.Id}");
        personalFood!.Name = request.FoodName;
        if (!string.IsNullOrWhiteSpace(request.ImageUri))
        {
            personalFood.ImageName = _fileService.AddImage(request.ImageUri, "PersonalFoodImage",
                Guid.NewGuid().ToString().Substring(0, 6));
        }

        var calculateIngredients = await _mediator.Send(new GetCalculatedIngredientQuery(request.Ingredients));
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
            personalFood.BakingTime = request.BakingTime;
            List<NotNegativeForDecimalTypes> foodNutrients = new List<NotNegativeForDecimalTypes>();
            foreach (var nut in calculateIngredients.CalculatedIngredient)
            {
                foodNutrients.Add(new NotNegativeForDecimalTypes(nut * 100 / foodFactors.AfterBaking));
            }

            foodNutrients[1] = new NotNegativeForDecimalTypes(remainedWater * 100 / foodFactors.AfterBaking);
            personalFood.NutrientValue = foodNutrients;
            // personalFood.IsCopyable = false;
            //Add PersonalFood Ingredient
            var foodIngredients = new List<PersonalFoodIngredient>();
            foreach (var i in personalFood.PersonalFoodIngredients)
            {
                if (request.Ingredients.Where(x => x.IngredientId == i.IngredientId.ToString()).ToList().Count == 1)
                {
                    var existFoodIngredient =
                        request.Ingredients.FirstOrDefault(x => x.IngredientId == i.IngredientId.ToString());
                    i.IngredientValue = new NotNegativeForDecimalTypes(existFoodIngredient!.Value);
                    i.MeasureUnitId = existFoodIngredient.MeasureUnitId.StringToObjectId();
                    foodIngredients.Add(i);
                }
            }


            foreach (var i in request.Ingredients)
            {
                if (!foodIngredients.Any(x => x.IngredientId == i.IngredientId.StringToObjectId()))
                {
                    foodIngredients.Add(new PersonalFoodIngredient
                    {
                        IngredientId = i.IngredientId.StringToObjectId(),
                        IngredientValue = new NotNegativeForDecimalTypes(i.Value),
                        MeasureUnitId = i.MeasureUnitId.StringToObjectId(),
                        Id = personalFood.Id
                    });
                }

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
            await _work.GenericRepository<PersonalFood>().UpdateOneAsync(
                x => x.Id == request.Id, personalFood,
                new Expression<Func<PersonalFood, object>>[]
                {
                    x => x.PersonalFoodIngredients,
                    x => x.NutrientValue,
                    x => x.BakingTime,
                    x => x.WeightAfterBaking,
                    x => x.EvaporatedWater,
                    x => x.WeightBeforeBaking,
                    x => x.Name,
                    x => x.ImageName,
                }, null, cancellationToken);


            var measureUnits = new List<string> { Keys.Grams, Keys.Ounces, Keys.Pounds };
            if (!string.IsNullOrWhiteSpace(personalFood.ParentFoodId.ToString()))
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
            else
            {
                // measureUnits = 
            }

            var result = personalFood.ToDto<PersonalFoodDto>();
            result.BakingTypeName = result.BakingType.ToDescription();
            result.MeasureUnits = measureUnits.Distinct().ToList();
            return result;
        }
        return null;
    }
}