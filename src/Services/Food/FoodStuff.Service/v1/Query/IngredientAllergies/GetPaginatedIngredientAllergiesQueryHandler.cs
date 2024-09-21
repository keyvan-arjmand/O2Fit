using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.IngredientAllergies
{
    public class GetPaginatedIngredientAllergiesQueryHandler: IRequestHandler<GetPaginatedIngredientAllergiesQuery, PageResult<IngredientAllergyDto>>, IScopedDependency
    {
        private readonly IRepository<IngredientAllergy> _repository;
        private readonly IMapper _mapper;

        public GetPaginatedIngredientAllergiesQueryHandler(IMapper mapper, IRepository<IngredientAllergy> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PageResult<IngredientAllergyDto>> Handle(GetPaginatedIngredientAllergiesQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.TableNoTracking.Include(x => x.Ingredient).ThenInclude(x=>x.Translation)
                .Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            var result = _mapper.Map<List<IngredientAllergy>, List<IngredientAllergyDto>>(data);
            return new PageResult<IngredientAllergyDto>
            {
                Count = data.Count,
                Items = result,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
        }
    }
}