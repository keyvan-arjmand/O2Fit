using AutoMapper;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Query.DietCategory
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, DietCategoryResultDto>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Diet.DietCategory> _repository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IRepository<Domain.Entities.Diet.DietCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DietCategoryResultDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {

            var cat = await _repository.Table
                .Include(t => t.NameTranslation)
                .Include(a => a.DescriptionTranslation)
                .FirstOrDefaultAsync(m => m.Id == request.Id,cancellationToken);

            var result = _mapper.Map<Domain.Entities.Diet.DietCategory, DietCategoryResultDto>(cat);
            if (result.ParentId >0 ||result.ParentId !=null)
            {
                result.ParentCategory = _mapper.Map<Domain.Entities.Diet.DietCategory, DietCategoryResultDto>(await
                        _repository.TableNoTracking
                            .Include(t => t.NameTranslation)
                            .Include(a => a.DescriptionTranslation)
                            .FirstOrDefaultAsync(x => x.Id == result.ParentId, cancellationToken));
            }

            return result;
        }
    }
}