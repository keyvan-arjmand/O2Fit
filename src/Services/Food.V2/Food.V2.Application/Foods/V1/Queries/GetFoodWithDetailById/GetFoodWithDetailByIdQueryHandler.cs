using Food.V2.Application.Dtos.Recipe;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Foods.V1.Queries.GetFoodWithDetailById;

public class GetFoodWithDetailByIdQueryHandler : IRequestHandler<GetFoodWithDetailByIdQuery, FullRecipeDto>
{
    private readonly IUnitOfWork _work;

    public GetFoodWithDetailByIdQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<FullRecipeDto> Handle(GetFoodWithDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException("Food not found");
        return result.ToDto<FullRecipeDto>();
    }
}