namespace Food.V2.Application.Ingredients.V1.Queries.GetIngredientById;

public class GetIngredientByIdQueryHandler: IRequestHandler<GetIngredientByIdQuery, GetIngredientByIdDto>
{
    private readonly IUnitOfWork _uow;

    public GetIngredientByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<GetIngredientByIdDto> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
    {
        var ingredient = await _uow.GenericRepository<Ingredient>().GetByIdAsync(request.Id, cancellationToken);
        if (ingredient == null)
            throw new NotFoundException(nameof(Ingredient), request.Id);

        var measureUnitDtos = new List<MeasureUnitDto>();        
        if (ingredient.MeasureUnitIds.Count > 0)
        {
            foreach (var id in ingredient.MeasureUnitIds)
            {
                var measureUnit = await _uow.GenericRepository<MeasureUnit>()
                    .GetByIdAsync(id.ToString(), cancellationToken);

                if (measureUnit == null)
                    throw new NotFoundException(nameof(MeasureUnit), id.ToString());

                var finalMeasureUnit = measureUnit.ToDto<MeasureUnitDto>();
                measureUnitDtos.Add(finalMeasureUnit);
            }
        }

        var getByIdIngredientDto = ingredient.ToDto<GetIngredientByIdDto>();
        getByIdIngredientDto.MeasureUnits.AddRange(measureUnitDtos);

        getByIdIngredientDto.NutrientValue = string.Join(",", ingredient.NutrientValue.Select(s => s.Value.ToString()));
        
        return getByIdIngredientDto;
    }
}