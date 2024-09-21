using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.RecipeSteps
{
    public class UpdateRecipeStepCommandHandler : IRequestHandler<UpdateRecipeStepCommand , Unit>, IScopedDependency
    {
        private readonly IRepository<RecipeStep> _repository;

        public UpdateRecipeStepCommandHandler(IRepository<RecipeStep> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateRecipeStepCommand request, CancellationToken cancellationToken)
        {
            var recipeStep = await _repository.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (recipeStep == null)
                throw new AppException(ApiResultStatusCode.ServerError);

            recipeStep.RecipeId = request.RecipeId;
            recipeStep.DescriptionId = request.DescriptionId;
            await _repository.UpdateAsync(recipeStep, cancellationToken);
            return Unit.Value;
        }
    }
}