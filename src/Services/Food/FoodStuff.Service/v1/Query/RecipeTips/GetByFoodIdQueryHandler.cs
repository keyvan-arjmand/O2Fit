using AutoMapper;
using FoodStuff.Service.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.RecipeTips
{
    public class GetByFoodIdQueryHandler : IRequestHandler<GetByFoodIdQuery, List<TipDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Tip> _repository;
        private readonly IMapper _mapper;
        public GetByFoodIdQueryHandler(IMapper mapper, IRepository<Domain.Entities.Food.Tip> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TipDto>> Handle(GetByFoodIdQuery request, CancellationToken cancellationToken)
        {
            var tips = await _repository.TableNoTracking
                .Include(x => x.Translation)
                .Include(x => x.Recipe)
                .Where(x => x.Recipe.FoodId == request.Id).ToListAsync(cancellationToken);
            return _mapper.Map<List<Domain.Entities.Food.Tip>, List<TipDto>>(tips);
        }
    }
}