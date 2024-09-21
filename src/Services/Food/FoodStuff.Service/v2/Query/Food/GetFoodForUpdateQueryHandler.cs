using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contracts;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodForUpdateQueryHandler : IRequestHandler<GetFoodForUpdateQuery, FoodDto>
    {
        private readonly IRepository<Domain.Entities.Food.Food> _repository;
        private readonly IMapper _mapper;

        public GetFoodForUpdateQueryHandler(IRepository<Domain.Entities.Food.Food> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FoodDto> Handle(GetFoodForUpdateQuery request, CancellationToken cancellationToken)
        {
            var food = await _repository.TableNoTracking.Include(x=>x.TranslationName).Include(x=>x.TranslationRecipe)
                .FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);
            
            if (food == null)
                return null;

            return _mapper.Map<Domain.Entities.Food.Food, FoodDto>(food);
        }
    }
}