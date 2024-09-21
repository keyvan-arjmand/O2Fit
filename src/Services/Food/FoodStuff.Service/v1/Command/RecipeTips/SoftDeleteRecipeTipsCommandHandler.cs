using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.v2.Command.RecipeSteps;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Command.RecipeTips
{
    public class SoftDeleteRecipeTipsCommandHandler : IRequestHandler<SoftDeleteRecipeTipsCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Tip> _repository;

        public SoftDeleteRecipeTipsCommandHandler(IRepository<Tip> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteRecipeTipsCommand request, CancellationToken cancellationToken)
        {
            var recipeStep = await _repository.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (recipeStep == null)
                throw new AppException(ApiResultStatusCode.ServerError);

            recipeStep.IsDelete = true;

            await _repository.UpdateAsync(recipeStep, cancellationToken);
            return Unit.Value;
        }
    }
}