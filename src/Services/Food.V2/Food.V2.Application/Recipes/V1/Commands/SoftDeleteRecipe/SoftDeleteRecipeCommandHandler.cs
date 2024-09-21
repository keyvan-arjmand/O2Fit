using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.Recipes.V1.Commands.SoftDeleteRecipe;

public class SoftDeleteRecipeCommandHandler : IRequestHandler<SoftDeleteRecipeCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteRecipeCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (result == null) throw new NotFoundException($"food not found {request.Id}");
        result.RecipeSteps.ForEach(x => x.IsDelete = false);
        result.RecipeTips.ForEach(x => x.IsDelete = false);
        await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().UpdateOneAsync(
            x => x.Id == request.Id, result,
            new Expression<Func<Domain.Aggregates.FoodAggregate.Food, object>>[]
            {
                x => x.RecipeSteps,
                x => x.RecipeTips
            }, null, cancellationToken);
    }
}