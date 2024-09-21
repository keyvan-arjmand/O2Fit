namespace Food.V2.Application.Foods.V1.Queries.GetFoodIngredientIds;

public class GetFoodIngredientIdsQueryHandler : IRequestHandler<GetFoodIngredientIdsQuery, List<string>>
{
    private readonly IUnitOfWork _uow;

    public GetFoodIngredientIdsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<string>> Handle(GetFoodIngredientIdsQuery request, CancellationToken cancellationToken)
    {
        var ingredientIds = await _uow.FoodRepository().GetFoodIngredientIdsById(request.FoodId, cancellationToken);
        if (ingredientIds == null || ingredientIds.Count == 0)
            throw new BadRequestException("Food have no ingredients");
        return ingredientIds;
    }
}