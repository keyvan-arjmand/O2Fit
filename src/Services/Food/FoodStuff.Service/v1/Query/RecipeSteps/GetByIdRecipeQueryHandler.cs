using AutoMapper;
using FoodStuff.Service.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.RecipeSteps
{
    public class GetByIdRecipeQueryHandler : IRequestHandler<GetByIdRecipeQuery, RecipeStepDto>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.RecipeStep> _repository;
        private readonly IMapper _mapper;

        public GetByIdRecipeQueryHandler(IRepository<Domain.Entities.Food.RecipeStep> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RecipeStepDto> Handle(GetByIdRecipeQuery request, CancellationToken cancellationToken)
        {
            var step = await _repository.TableNoTracking
                .Include(x => x.Translation)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return _mapper.Map<Domain.Entities.Food.RecipeStep, RecipeStepDto>(step);
        }
    }
}