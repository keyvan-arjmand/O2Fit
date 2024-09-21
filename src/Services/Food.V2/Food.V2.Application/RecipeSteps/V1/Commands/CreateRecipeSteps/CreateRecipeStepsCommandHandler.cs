using System.Transactions;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;
using Food.V2.Domain.Enums;

namespace Food.V2.Application.RecipeSteps.V1.Commands.CreateRecipeSteps;

public class CreateRecipeStepsCommandHandler : IRequestHandler<CreateRecipeStepsCommand>
{
    private readonly IUnitOfWork _work;

    public CreateRecipeStepsCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(CreateRecipeStepsCommand request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.FoodId, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.FoodId}");
        
        request.Steps.ForEach(x => x.Id = ObjectId.GenerateNewId().ToString());
        food.RecipeSteps.AddRange(request.Steps.MapTo<List<FoodTranslation>, List<TranslationDto>>());
        await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .UpdateOneAsync(x => x.Id == request.FoodId, food,
                new Expression<Func<Domain.Aggregates.FoodAggregate.Food, object>>[]
                {
                    x => x.RecipeSteps
                }, null, cancellationToken);
    }
}