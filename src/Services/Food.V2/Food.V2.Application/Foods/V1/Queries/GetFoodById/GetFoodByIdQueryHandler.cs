using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Foods.V1.Queries.GetFoodById;

public class GetFoodByIdQueryHandler : IRequestHandler<GetFoodByIdQuery, FoodWithDetailDto>
{
    private readonly IUnitOfWork _work;

    public GetFoodByIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<FoodWithDetailDto> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.FoodAggregation().GetFoodWithDetailById(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("Food not found");
        return result;
    }
}