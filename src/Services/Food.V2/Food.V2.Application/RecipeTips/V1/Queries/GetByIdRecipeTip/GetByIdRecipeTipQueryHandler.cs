using Food.V2.Application.Dtos.Recipe;
using Food.V2.Application.Dtos.Translation;
using Food.V2.Domain.Aggregates.RecipeAggregate;

namespace Food.V2.Application.RecipeTips.V1.Queries.GetByIdRecipeTip;

public class GetByIdRecipeTipQueryHandler : IRequestHandler<GetByIdRecipeTipQuery, RecipeTipSingleDto>
{
    private readonly IUnitOfWork _work;

    public GetByIdRecipeTipQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<RecipeTipSingleDto> Handle(GetByIdRecipeTipQuery request, CancellationToken cancellationToken)
    {
        var food = await _work.GenericRepository<Domain.Aggregates.FoodAggregate.Food>()
            .GetByIdAsync(request.FoodId, cancellationToken);
        if (food == null) throw new NotFoundException($"food not found {request.FoodId}");
        var tip = food.RecipeTips.FirstOrDefault(x => x.Id == request.Id);
        if (tip == null) throw new NotFoundException("Not Found Tip");
        return new RecipeTipSingleDto
        {
            FoodId = food.Id,
            RecipeTip = tip.ToDto<TranslationDto>() ,
        };
    }
}