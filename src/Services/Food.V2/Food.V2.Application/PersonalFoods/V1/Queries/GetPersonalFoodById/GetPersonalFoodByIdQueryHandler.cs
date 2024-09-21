using Common.Constants.Food;
using Food.V2.Application.Dtos.PersonalFood;
using Food.V2.Domain.Aggregates.PersonalFoodAggregate;

namespace Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodById;

public class GetPersonalFoodByIdQueryHandler : IRequestHandler<GetPersonalFoodByIdQuery, PersonalFoodDto>
{
    private readonly IUnitOfWork _work;

    public GetPersonalFoodByIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PersonalFoodDto> Handle(GetPersonalFoodByIdQuery request, CancellationToken cancellationToken)
    {
        var measureUnits = new List<string> { Keys.Grams, Keys.Ounces, Keys.Pounds };
        var personalFood = await _work.GenericRepository<PersonalFood>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (personalFood != null)
        {
            if (!string.IsNullOrWhiteSpace(personalFood.ParentFoodId.ToString()))
            {
                var filter =
                    Builders<Domain.Aggregates.FoodAggregate.Food>.Filter.Eq(x => x.Id, personalFood.ParentFoodId.ObjectIdToString());
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
            result.MeasureUnits = measureUnits.Distinct().ToList();
            return result;
        }
        return new PersonalFoodDto();
    }
}