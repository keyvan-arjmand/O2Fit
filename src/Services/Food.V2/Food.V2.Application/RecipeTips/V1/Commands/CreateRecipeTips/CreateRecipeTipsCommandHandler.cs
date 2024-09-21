using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;
using Food.V2.Domain.Enums;

namespace Food.V2.Application.RecipeTips.V1.Commands.CreateRecipeTips;

public class CreateRecipeTipsCommandHandler : IRequestHandler<CreateRecipeTipsCommand>
{
    private readonly IUnitOfWork _work;

    public CreateRecipeTipsCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task Handle(CreateRecipeTipsCommand request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.FoodId, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.FoodId}");
        
        request.Tips.ForEach(x => x.Id = ObjectId.GenerateNewId().ToString());
        food.RecipeTips.AddRange(request.Tips.MapTo<List<FoodTranslation>, List<TranslationDto>>());
        await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .UpdateOneAsync(x => x.Id == request.FoodId, food,
                new Expression<Func<Domain.Aggregates.FoodAggregate.Food, object>>[]
                {
                    x => x.RecipeTips
                }, null, cancellationToken);
        }
    }
