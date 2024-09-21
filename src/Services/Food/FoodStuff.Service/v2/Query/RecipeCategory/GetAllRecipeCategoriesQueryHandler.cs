using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Query.RecipeCategory
{
    public class GetAllRecipeCategoriesQueryHandler : IRequestHandler<GetAllRecipeCategoriesQuery, List<RecipeCategoryDto>>, IScopedDependency
    {
        private readonly IRecipeCategory _recipeCategory;
        private readonly IMapper _mapper;

        public GetAllRecipeCategoriesQueryHandler(IRecipeCategory recipeCategory, IMapper mapper)
        {
            _recipeCategory = recipeCategory;
            _mapper = mapper;
        }

        public async Task<List<RecipeCategoryDto>> Handle(GetAllRecipeCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allRecipes = await _recipeCategory.TableNoTracking.Include(x => x.NameTranslation)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<RecipeCategore>, List<RecipeCategoryDto>>(allRecipes);
        }
    }
}