using AutoMapper;
using FoodStuff.Data.Contracts;
using FoodStuff.Service.Models;
using FoodStuff.Service.v2.Query.RecipeCategory;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v2.Query.Recipe
{
    public class GetAllRecipeQueryHandler : IRequestHandler<GetAllRecipeQuery, List<RecipeGetAllDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Recipe> _repositoryRecipe;
        private readonly IMapper _mapper;

        public GetAllRecipeQueryHandler(IRepository<Domain.Entities.Food.Recipe> repositoryRecipe, IMapper mapper)
        {
            _repositoryRecipe = repositoryRecipe;
            _mapper = mapper;
        }

        public async Task<List<RecipeGetAllDto>> Handle(GetAllRecipeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryRecipe.TableNoTracking
                .Include(x => x.Food).ThenInclude(x => x.TranslationName)
                .Include(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                .Include(x => x.Tips).ThenInclude(x => x.Translation)
                .Skip((request.Page - 1 ?? 0) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<Domain.Entities.Food.Recipe>, List<RecipeGetAllDto>>(result);
        }
    }
}