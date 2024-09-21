namespace Food.V2.Application.Ingredients.V1.Queries.GetCalculatedIngredient;

public class GetCalculatedIngredientQueryHandler : IRequestHandler<GetCalculatedIngredientQuery, CalculatedIngredientDto>
{
    private readonly IUnitOfWork _uow;

    public GetCalculatedIngredientQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<CalculatedIngredientDto> Handle(GetCalculatedIngredientQuery request, CancellationToken cancellationToken)
    {
        var calculatedIngredient = new CalculatedIngredientDto();

        var result = new decimal[34];

        var listOfIngredient = new List<decimal>();
        var listOfMeasureUnit = new List<decimal>();

        foreach (var ingredientMeasureUnitDto in request.IngredientMeasureUnitList)
        {
            var ingredient = await _uow.GenericRepository<Ingredient>()
                .GetByIdAsync(ingredientMeasureUnitDto.IngredientId, cancellationToken);
            if (ingredient != null)
                listOfIngredient.AddRange(ingredient.NutrientValue.Select(s=>s.Value).ToList());

            var measureUnit = await _uow.GenericRepository<MeasureUnit>()
                .GetByIdAsync(ingredientMeasureUnitDto.MeasureUnitId, cancellationToken);
            if (measureUnit != null)
                listOfMeasureUnit.Add(measureUnit.Value);
        }

        decimal sumWeight = 0;
        foreach (var dto in request.IngredientMeasureUnitList)
        {
            result = NutrientCalculate.SumNutrient(result, listOfIngredient, listOfMeasureUnit, dto.Value,
                ref sumWeight);
        }

        calculatedIngredient.CalculatedIngredient = result.ToList();
        calculatedIngredient.SumWeight = sumWeight;
        return calculatedIngredient;
    }
}