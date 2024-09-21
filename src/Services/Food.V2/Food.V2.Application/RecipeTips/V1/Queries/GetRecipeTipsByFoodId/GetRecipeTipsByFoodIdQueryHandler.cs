using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeTips.V1.Queries.GetRecipeTipsByFoodId;

public class GetRecipeTipsByFoodIdQueryHandler : IRequestHandler<GetRecipeTipsByFoodIdQuery, List<TranslationDto>>
{
    private readonly IUnitOfWork _work;

    public GetRecipeTipsByFoodIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<TranslationDto>> Handle(GetRecipeTipsByFoodIdQuery request,
        CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.Id}");
        return food.RecipeTips.MapTo<List<TranslationDto>, List<FoodTranslation>>().ToList();
    }
}