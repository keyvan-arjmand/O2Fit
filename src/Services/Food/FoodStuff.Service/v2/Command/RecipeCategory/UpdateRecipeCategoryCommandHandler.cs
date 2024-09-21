using System.Threading;
using System.Threading.Tasks;
using Common;
using FoodStuff.Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.RecipeCategory
{
    public class UpdateRecipeCategoryCommandHandler : IRequestHandler<UpdateRecipeCategoryCommand, Unit>, IScopedDependency
    {
        private readonly IRecipeCategory _recipeCategory;

        public UpdateRecipeCategoryCommandHandler(IRecipeCategory recipeCategory)
        {
            _recipeCategory = recipeCategory;
        }

        public async Task<Unit> Handle(UpdateRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var recipeCategory = await _recipeCategory.Table.FirstOrDefaultAsync(x=>x.Id == request.UpdateRecipeCategoryDto.Id,
                cancellationToken);

            if (recipeCategory != null)
            {
                recipeCategory.IsActive = request.UpdateRecipeCategoryDto.IsActive;
                recipeCategory.NameId = request.TranslationId;
                await _recipeCategory.UpdateAsync(recipeCategory, cancellationToken);
            }

            return Unit.Value;
        }
    }
}