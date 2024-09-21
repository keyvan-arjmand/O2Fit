using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v2.Command.Recipes
{
    public class DeleteRecipeCommandHandler: IRequestHandler<DeleteRecipeCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRepository<RecipeStep> _recipeStepRepository;
        private readonly IRepository<Tip> _tipRepository;

        public DeleteRecipeCommandHandler(IRepository<Tip> tipRepository, IRepository<RecipeStep> recipeStepRepository, IRepository<Recipe> recipeRepository)
        {
            _tipRepository = tipRepository;
            _recipeStepRepository = recipeStepRepository;
            _recipeRepository = recipeRepository;
        }


        public async Task<Unit> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var tips = await _tipRepository.Table.IgnoreQueryFilters().Where(x => x.RecipeId == request.Id).ToListAsync(cancellationToken);
                if (tips.Count != 0)
                {
                    await _tipRepository.DeleteRangeAsync(tips, cancellationToken);
                }

                var steps = await _recipeStepRepository.Table.IgnoreQueryFilters().Where(x => x.RecipeId == request.Id)
                    .ToListAsync(cancellationToken);
                if (steps.Count != 0)
                {
                    await _recipeStepRepository.DeleteRangeAsync(steps, cancellationToken);
                }

                var recipe = await _recipeRepository.Table.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (recipe != null)
                {
                    await _recipeRepository.DeleteAsync(recipe, cancellationToken);
                }
                return Unit.Value;
            }
            catch (System.Exception)
            {
                throw new AppException(ApiResultStatusCode.ServerError);
            }
        }
    }
}