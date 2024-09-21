using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Command.IngredientAllergies
{
    public class CreateIngredientAllergiesCommandHandler : IRequestHandler<CreateIngredientAllergiesCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<IngredientAllergy> _repository;

        public CreateIngredientAllergiesCommandHandler(IRepository<IngredientAllergy> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateIngredientAllergiesCommand request, CancellationToken cancellationToken)
        {
            var id = await _repository.TableNoTracking.IgnoreQueryFilters().Select(s=>s.Id)
                .CountAsync(cancellationToken: cancellationToken).ConfigureAwait(false) + 1;
            var ingredientAllergies = await _repository.Table.
                FirstOrDefaultAsync(x => x.IngredientId == request.IngredientId,
                cancellationToken).ConfigureAwait(false);
            if (request.IsChecked)
            {
                if (ingredientAllergies == null)
                {
                    var ingredientAllergiesToCreate = new IngredientAllergy
                    {
                        IngredientId = request.IngredientId,
                        Id = id
                    };
                    await _repository.AddAsync(ingredientAllergiesToCreate, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    throw new AppException(ApiResultStatusCode.BadRequest);
                }
            }
            else
            {
                if (ingredientAllergies != null)
                {
                    ingredientAllergies.IsDelete = true;
                    await _repository.UpdateAsync(ingredientAllergies, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    throw new AppException(ApiResultStatusCode.BadRequest);
                }
            }

            return Unit.Value;
        }
    }
}