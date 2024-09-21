using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v1.Command.RecipeSteps
{
    public class UpdateRangeRecipeStepCommandHandler : IRequestHandler<UpdateRangeRecipeStepCommand>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.RecipeStep> _repositorysteps;
        private readonly IRepository<Domain.Entities.Food.Recipe> _repositoryRecepie;
        public UpdateRangeRecipeStepCommandHandler(IRepository<Domain.Entities.Food.RecipeStep> repositorysteps, IRepository<Domain.Entities.Food.Recipe> repositoryRecepie)
        {
            _repositorysteps = repositorysteps;
            _repositoryRecepie = repositoryRecepie;
        }

        public async Task<Unit> Handle(UpdateRangeRecipeStepCommand request, CancellationToken cancellationToken)
        {
            var recipe = await
                _repositoryRecepie.Table.FirstOrDefaultAsync(x => x.FoodId == request.FoodId, cancellationToken);
            if (recipe == null) throw new BadRequestException();

            var recipeSteps = await _repositorysteps.Table.Include(x => x.Recipe)
                .Where(x => request.RecipeSteps.Select(x => x.Id).ToList().Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var i in recipeSteps)
            {
                i.RecipeId = recipe.Id;
            }
            await _repositorysteps.UpdateRangeAsync(recipeSteps, cancellationToken);
            return Unit.Value;
        }
    }
}