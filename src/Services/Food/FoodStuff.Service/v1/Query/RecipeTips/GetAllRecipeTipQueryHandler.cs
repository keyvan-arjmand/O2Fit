using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Models;
using FoodStuff.Service.v1.Command.RecipeSteps;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.RecipeTips
{
    public class GetAllRecipeTipQueryHandler : IRequestHandler<GetAllRecipeTipQuery, List<TipDto>>, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Food.Tip> _repository;
        public GetAllRecipeTipQueryHandler(IRepository<Domain.Entities.Food.Tip> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TipDto>> Handle(GetAllRecipeTipQuery request, CancellationToken cancellationToken)
        {
            var tips = await _repository.TableNoTracking
                .Include(x => x.Translation)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<Domain.Entities.Food.Tip>, List<TipDto>>(tips);
        }
    }
}