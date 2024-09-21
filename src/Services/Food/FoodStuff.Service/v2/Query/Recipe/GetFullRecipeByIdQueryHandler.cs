using AutoMapper;
using FoodStuff.Service.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v2.Query.Recipe
{
    public class GetFullRecipeByIdQueryHandler : IRequestHandler<GetFullRecipeByIdQuery, GetFullRecipeById>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Recipe> _repositoryRecipe;
        private readonly IMapper _mapper;

        public GetFullRecipeByIdQueryHandler(IRepository<Domain.Entities.Food.Recipe> repositoryRecipe, IMapper mapper)
        {
            _repositoryRecipe = repositoryRecipe;
            _mapper = mapper;
        }

        public async Task<GetFullRecipeById> Handle(GetFullRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryRecipe.TableNoTracking
                .Include(x => x.Food).ThenInclude(x => x.TranslationName)
                .Include(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                .Include(x => x.Tips).ThenInclude(x => x.Translation)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return _mapper.Map<Domain.Entities.Food.Recipe, GetFullRecipeById>(result);

        }
    }
}