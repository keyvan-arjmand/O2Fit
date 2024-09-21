namespace Food.V2.Application.Common.Interfaces.Persistence.Repositories;

public interface IIngredientRepository : IGenericRepository<Ingredient>
{
  // Task<GetIngredientByIdDto> GetFullIngredientByIdAsync(string id, CancellationToken cancellationToken);
}