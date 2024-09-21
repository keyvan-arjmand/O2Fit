using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace FoodStuff.Service.v1.Query.RecipeSteps
{
    public class GetByRecipeIdQueryHandler : IRequestHandler<GetByFoodIdQuery, List<RecipeStepDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.RecipeStep> _repositorysteps;
        private readonly IMapper _mapper;
        public GetByRecipeIdQueryHandler(IMapper mapper, IRepository<Domain.Entities.Food.RecipeStep> repositorysteps)
        {
            _repositorysteps = repositorysteps;
            _mapper = mapper;
        }

        public async Task<List<RecipeStepDto>> Handle(GetByFoodIdQuery request, CancellationToken cancellationToken)
        {
            var steps = await _repositorysteps.TableNoTracking
                .Include(x => x.Translation)
                .Include(x => x.Recipe)
                .Where(x => x.Recipe.FoodId == request.Id).ToListAsync(cancellationToken);

            return _mapper.Map<List<Domain.Entities.Food.RecipeStep>, List<RecipeStepDto>>(steps);
        }
    }
}
