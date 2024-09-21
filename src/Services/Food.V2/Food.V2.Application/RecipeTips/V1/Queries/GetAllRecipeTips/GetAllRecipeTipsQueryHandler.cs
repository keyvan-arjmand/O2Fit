using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeTips.V1.Queries.GetAllRecipeTips;

public class GetAllRecipeTipsQueryHandler : IRequestHandler<GetAllRecipeTipsQuery, List<RecipeTipDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllRecipeTipsQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<List<RecipeTipDto>> Handle(GetAllRecipeTipsQuery request, CancellationToken cancellationToken)
    {
        var list = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().GetAllAsync(cancellationToken);
        return list.ToDto<RecipeTipDto>().ToList();
    }
}