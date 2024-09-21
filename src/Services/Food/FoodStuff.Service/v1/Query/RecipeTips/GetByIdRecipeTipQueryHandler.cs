using AutoMapper;
using FoodStuff.Service.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.RecipeTips
{
    public class GetByIdRecipeTipQueryHandler : IRequestHandler<GetByIdRecipeTipQuery, TipDto>, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Food.Tip> _repository;
        public GetByIdRecipeTipQueryHandler(IRepository<Domain.Entities.Food.Tip> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TipDto> Handle(GetByIdRecipeTipQuery request, CancellationToken cancellationToken)
        {
            var tip = await _repository.TableNoTracking
                .Include(x => x.Translation)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return _mapper.Map<Domain.Entities.Food.Tip, TipDto>(tip);
        }
    }
}