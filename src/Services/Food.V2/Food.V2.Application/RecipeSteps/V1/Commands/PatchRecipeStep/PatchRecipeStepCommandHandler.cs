using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeSteps.V1.Commands.PatchRecipeStep;

public class PatchRecipeStepCommandHandler:IRequestHandler<PatchRecipeStepCommand>
{
    private readonly IUnitOfWork _work;

    public PatchRecipeStepCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(PatchRecipeStepCommand request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.FoodId, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.FoodId}");
        foreach (var step in food.RecipeSteps)
        {
            var patch = request.Steps.FirstOrDefault(x => x.Id == step.Id);
            if (patch != null)
            {
                step.Arabic = patch.Arabic;
                step.English = patch.English;
                step.Persian = patch.Persian;
            }
        }
        await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>().UpdateOneAsync(x => x.Id == request.FoodId, food,
            new Expression<Func<Domain.Aggregates.FoodAggregate.Food, object>>[]
            {
                x => x.RecipeSteps
            }, null, cancellationToken);
    }
}