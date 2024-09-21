using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.Recipes
{
    public class UpdateRecipeCommandHandler: IRequestHandler<UpdateRecipeCommand , Unit>, IScopedDependency
    {
        private readonly IRepository<Recipe> _repository;

        public UpdateRecipeCommandHandler(IRepository<Recipe> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _repository.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (recipe == null)
                throw new AppException(ApiResultStatusCode.ServerError);

            recipe.Status = request.Status;
            recipe.FoodId = request.FoodId;

            await _repository.UpdateAsync(recipe, cancellationToken);
            return Unit.Value;
        }
    }
}