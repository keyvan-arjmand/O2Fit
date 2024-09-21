using System.Threading;
using System.Threading.Tasks;
using Common;
using FoodStuff.Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.RecipeCategory
{
    public class DeleteRecipeCategoryCommandHandler : IRequestHandler<DeleteRecipeCategoryCommand , Unit>, IScopedDependency
    {
        private readonly IRecipeCategory _recipeCategory;

        public DeleteRecipeCategoryCommandHandler(IRecipeCategory recipeCategory)
        {
            _recipeCategory = recipeCategory;
        }

        public async Task<Unit> Handle(DeleteRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var recipeCategory =
                await _recipeCategory.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (recipeCategory != null)
            {
                recipeCategory.IsDelete = true;
                await _recipeCategory.UpdateAsync(recipeCategory, cancellationToken);
            }

            return Unit.Value;
        }
    }
}