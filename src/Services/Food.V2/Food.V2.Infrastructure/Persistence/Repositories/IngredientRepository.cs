using Food.V2.Application.Common.Exceptions;
using Food.V2.Application.Dtos.Ingredients;
using Food.V2.Domain.Aggregates.MeasureUnitAggregate;

namespace Food.V2.Infrastructure.Persistence.Repositories;

public class IngredientRepository : GenericRepository<Ingredient> , IIngredientRepository
{
    public IngredientRepository(IMediator mediator, IConfiguration configuration, ICurrentUserService currentUserService) : base(mediator, configuration, currentUserService)
    {
    }

    //public async Task<GetIngredientByIdDto> GetFullIngredientByIdAsync(string id, CancellationToken cancellationToken)
    //{
    //    var ingredient = await GetByIdAsync(id, cancellationToken);
    //    if (ingredient == null)
    //        throw new NotFoundException(nameof(Ingredient), id);
    //    var result = await Collection.Aggregate().Lookup<Ingredient,MeasureUnit, GetIngredientByIdDto>(co)
    //}
}