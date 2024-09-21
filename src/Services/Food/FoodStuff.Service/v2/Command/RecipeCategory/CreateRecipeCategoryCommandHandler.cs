using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v2.Command.RecipeCategory
{
    public class CreateRecipeCategoryCommandHandler : IRequestHandler<CreateRecipeCategoryCommand, Unit>, IScopedDependency
    {
        private readonly IRecipeCategory _recipeCategory;

        public CreateRecipeCategoryCommandHandler(IRecipeCategory recipeCategory)
        {
            _recipeCategory = recipeCategory;
        }

        public async Task<Unit> Handle(CreateRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var recipeCategory = new RecipeCategore
            {
                IsActive = request.RecipeCategory.IsActive,
                NameId = request.TranslationId
            };
            await _recipeCategory.AddAsync(recipeCategory, cancellationToken);
            return Unit.Value;
        }
    }
}