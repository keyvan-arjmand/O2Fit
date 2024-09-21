using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeSteps.V1.Commands.SoftDeleteRecipeStep;

public class SoftDeleteRecipeStepCommandHandler : IRequestHandler<SoftDeleteRecipeStepCommand>
{
    private readonly IUnitOfWork _work;

    public SoftDeleteRecipeStepCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(SoftDeleteRecipeStepCommand request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.FoodId, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.Id}");
        food.RecipeSteps.FirstOrDefault(x => x.Id == request.Id)!.IsDelete = true;
        await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().UpdateOneAsync(x => x.Id == request.FoodId, food,
            new Expression<Func<Domain.Aggregates.FoodAggregate.Food, object>>[]
            {
                x => x.RecipeSteps
            }, null, cancellationToken);
    }
}