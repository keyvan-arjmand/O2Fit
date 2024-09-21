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

namespace FoodStuff.Service.v2.Query.RecipeCategory
{
    public class GetRecipeCategoryByIdQueryHandler : IRequestHandler<GetRecipeCategoryByIdQuery, RecipeCategoryDto>, IScopedDependency
    {
        private readonly IRecipeCategory _recipeCategory;
        private readonly IMapper _mapper;

        public GetRecipeCategoryByIdQueryHandler(IRecipeCategory recipeCategory, IMapper mapper)
        {
            _recipeCategory = recipeCategory;
            _mapper = mapper;
        }

        public async Task<RecipeCategoryDto> Handle(GetRecipeCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeCategory.TableNoTracking.Include(x => x.NameTranslation)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return recipe == null ? new RecipeCategoryDto() : _mapper.Map<RecipeCategore, RecipeCategoryDto>(recipe);
        }
    }
}