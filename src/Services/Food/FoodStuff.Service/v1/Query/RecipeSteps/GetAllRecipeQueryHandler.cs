using AutoMapper;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.RecipeSteps
{
    public class GetAllRecipeQueryHandler : IRequestHandler<GetAllRecipeQuery, List<RecipeStepDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.RecipeStep> _repository;
        private readonly IMapper _mapper;

        public GetAllRecipeQueryHandler(IRepository<Domain.Entities.Food.RecipeStep> repository, IMapper mapper, IRedisCacheClient redisCacheClient, IWebHostEnvironment environment)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RecipeStepDto>> Handle(GetAllRecipeQuery request, CancellationToken cancellationToken)
        {

            return _mapper.Map<List<Domain.Entities.Food.RecipeStep>, List<RecipeStepDto>>(
                await _repository.TableNoTracking.Include(x=>x.Translation).ToListAsync(cancellationToken));
        }
    }
}