using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v2.Query.RecipeCategory
{
    public class GetAllActiveRecipeCategoryQueryHandler : IRequestHandler<GetAllActiveRecipeCategoryQuery, List<RecipeCategoryDto>>, IScopedDependency
    {
        private readonly IRecipeCategory _recipeCategory;
        private readonly IMapper _mapper;

        public GetAllActiveRecipeCategoryQueryHandler(IRecipeCategory recipeCategory, IMapper mapper)
        {
            _recipeCategory = recipeCategory;
            _mapper = mapper;
        }

        public async Task<List<RecipeCategoryDto>> Handle(GetAllActiveRecipeCategoryQuery request, CancellationToken cancellationToken)
        {
            var activeRecipes = await _recipeCategory.TableNoTracking.Include(x=>x.NameTranslation).Where(x=>x.IsActive).ToListAsync(cancellationToken);

            return _mapper.Map<List<RecipeCategore>,List<RecipeCategoryDto>>(activeRecipes);
        }
    }
}