using Food.V2.Application.Ingredients.V1.Queries.GetCalculatedIngredient;

namespace Food.V2.Application.PersonalFoods.V1.Queries.CalculateNutrients;

public class CalculateNutrientsQueryHandler:IRequestHandler<CalculateNutrientsQuery,List<decimal>>
{
    private readonly IUnitOfWork _work;
    private readonly IMediator _mediator;

    public CalculateNutrientsQueryHandler(IUnitOfWork work, IMediator mediator)
    {
        _work = work;
        _mediator = mediator;
    }

    public async Task<List<decimal>> Handle(CalculateNutrientsQuery request, CancellationToken cancellationToken)
    {
        var calculateIngredients = await _mediator.Send(new GetCalculatedIngredientQuery(request.Ingredients));
        var foodFactors = new FoodParametersDto
        {
            BakingTimeInMinute = Convert.ToDecimal(request.BakingTime.TotalMinutes),
            BakingRatio = Convert.ToDecimal(request.BakingType.GetDescriptionValue()),
            FoodNutrients = calculateIngredients.CalculatedIngredient,
            FoodWeight = calculateIngredients.SumWeight
        }.FoodWeightsCalculation();
        var remainedWater = calculateIngredients.CalculatedIngredient[1] - foodFactors.EvaporatedWater;
        if (remainedWater < 0) throw new AppException($"Remained Water is a{remainedWater}");
            List<decimal> foodNutrients = new List<decimal>();
            foreach (var nut in calculateIngredients.CalculatedIngredient)
            {
                foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
            }
            foodNutrients[1] = remainedWater*100 / foodFactors.AfterBaking;
            return foodNutrients;
    }
}