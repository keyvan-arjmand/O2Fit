using Common.Constants.Food;
using Food.V2.Application.Dtos.PersonalFood;
using Food.V2.Domain.Aggregates.PersonalFoodAggregate;

namespace Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodsByUserId;

public class
    GetPersonalFoodsByUserIdQueryHandler : IRequestHandler<GetPersonalFoodsByUserIdQuery, List<PersonalFoodDto>>
{
    private readonly IUnitOfWork _work;
    private readonly IMediator _mediator;

    public GetPersonalFoodsByUserIdQueryHandler(IUnitOfWork work, IMediator mediator)
    {
        _work = work;
        _mediator = mediator;
    }

    public async Task<List<PersonalFoodDto>> Handle(GetPersonalFoodsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var filter = Builders<PersonalFood>.Filter.Eq(x => x.CreatedById, request.UserId.StringToObjectId());
        var result = await _work.GenericRepository<PersonalFood>()
            .GetListOfDocumentsByFilterAsync(filter, cancellationToken);
        var personalFoodDto = new List<PersonalFood>();
        foreach (var i in result)
        {
            var measureUnits = new List<string> { Keys.Grams, Keys.Ounces, Keys.Pounds };
            if (!string.IsNullOrEmpty(i.ParentFoodId.ToString()))
            {
                var filterPersonalFoodIngredients =
                    Builders<Domain.Aggregates.FoodAggregate.Food>.Filter.Eq(x => x.Id,
                        i.ParentFoodId.ObjectIdToString());
                var personalFoodIngredients =
                    await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
                        .GetListOfDocumentsByFilterAsync(filterPersonalFoodIngredients, cancellationToken);

                if (personalFoodIngredients.Any())
                {
                    measureUnits.AddRange(personalFoodIngredients
                        .Select(m => m.MeasureUnitIds.ConvertAll(x => x.ToString()).ToList()).SingleOrDefault()!);
                }

                var foodIngredients = new List<PersonalFoodIngredient>();
                foreach (var item in i.PersonalFoodIngredients)
                {
                    var ingredient = await _work.GenericRepository<Ingredient>()
                        .GetByIdAsync(item.IngredientId.ToString()!, cancellationToken);
                    ingredient!.MeasureUnitIds.Add(item.MeasureUnitId); //for food Ingredient
                    var measureUnitIds = ingredient.MeasureUnitIds.ConvertAll(x => x.ToString());
                    var filterMeasureUnitIds =
                        Builders<MeasureUnit>.Filter.Where(x =>
                            measureUnitIds.Contains(x.Id)); // for IngredientMeasureUnits
                    var ingredientMeasureUnit = await _work.GenericRepository<MeasureUnit>()
                        .GetListOfDocumentsByFilterAsync(filterMeasureUnitIds, cancellationToken);
                    var foodIng = new PersonalFoodIngredient()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        IngredientId = item.IngredientId,
                        IngredientValue = new NotNegativeForDecimalTypes(item.IngredientValue),
                        MeasureUnitId =
                            ingredientMeasureUnit.FirstOrDefault(x => x.Id == item.MeasureUnitId.ToString())!.Id
                                .StringToObjectId(),
                        MeasureUnitTranslation =
                            ingredientMeasureUnit.FirstOrDefault(x => x.Id == item.MeasureUnitId.ToString())!
                                .Translation
                                .MapTo<PersonalFoodTranslation, MeasureUnitTranslation>(),
                        MeasureUnitValue =
                            ingredientMeasureUnit.FirstOrDefault(x => x.Id == item.MeasureUnitId.ToString())!.Value,
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

                i.PersonalFoodIngredients = foodIngredients;
                personalFoodDto.Add(i);
            }
        }

        var foodResult = personalFoodDto.ToDto<PersonalFoodDto>().ToList();
        return foodResult;
    }
}