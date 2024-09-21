using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Recipes.V1.Commands.ChangeRecipeStatus;

public class ChangeRecipeStatusCommandHandler : IRequestHandler<ChangeRecipeStatusCommand>
{
    private readonly IUnitOfWork _work;

    public ChangeRecipeStatusCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(ChangeRecipeStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.FoodId, cancellationToken);
        if (result == null) throw new NotFoundException($"food not found {request.FoodId}");
        result.RecipeStatus = request.Status;
        await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().UpdateOneAsync(
            x => x.Id == request.FoodId, result,
            new Expression<Func<Domain.Aggregates.FoodAggregate.Food, object>>[]
            {
                x => x.RecipeStatus
            }, null, cancellationToken);
    }
}